using System;
using System.Collections.Generic;
using BokuMono;
using BokuMono.Data;
using HarmonyLib;
using TMPro;
using UnityEngine;

namespace WindmillETA
{
    /// <summary>
    /// 既存UIを移動せず、加工中一覧に独立した完了予定欄を追加します。
    /// </summary>
    [HarmonyPatch(
        typeof(UIWindmillCraftLine),
        "ObjUpdate",
        new[]
        {
            typeof(WindmillCraftingMasterData),
            typeof(WindmillCraftingManager.CraftingData)
        })]
    public static class WindmillCraftLinePatch
    {
        private const string EtaObjectName = "WindmillETA_Completion";

        private sealed class PendingRow
        {
            internal UIWindmillCraftLine Row;
            internal long EndTicks;
            internal bool Warned;
        }

        private static readonly Dictionary<int, PendingRow> Rows = new();
        private static readonly List<int> RemovedRows = new();

        // Refresh only visible, registered crafting rows. Initialization and opening
        // animations can change the native font, color and text bounds after ObjUpdate.
        internal static void RefreshVisibleRows()
        {
            RemovedRows.Clear();
            foreach (var pair in Rows)
            {
                var pending = pair.Value;
                var row = pending.Row;
                if (row == null || row.obj == null || row.timer == null)
                {
                    RemovedRows.Add(pair.Key);
                    continue;
                }
                if (!row.obj.activeInHierarchy || !row.timer.gameObject.activeInHierarchy) continue;
                try
                {
                    var now = DateManager.Instance;
                    if (now == null) continue;
                    if (pending.EndTicks <= now.Now.Ticks)
                    {
                        var old = row.timer.transform.Find(EtaObjectName);
                        if (old != null) old.gameObject.SetActive(false);
                        RemovedRows.Add(pair.Key);
                        continue;
                    }
                    Render(row, pending.EndTicks);
                }
                catch (Exception ex)
                {
                    if (!pending.Warned)
                    {
                        pending.Warned = true;
                        Plugin.LogSource.LogWarning($"[ETA-UI] Row {pair.Key} refresh failed; retrying: {ex}");
                    }
                }
            }
            foreach (var id in RemovedRows) Rows.Remove(id);
        }

        private static void Prefix(UIWindmillCraftLine __instance)
        {
            if (__instance != null) Rows.Remove(__instance.GetInstanceID());
            if (__instance?.timer == null)
                return;

            // 行はタブ切替などで再利用されるため、
            // 前回表示したETAをいったん非表示にする。
            var existing = __instance.timer.transform.Find(EtaObjectName);
            if (existing != null)
            {
                existing.gameObject.SetActive(false);
            }
        }

        public static void Postfix(
            UIWindmillCraftLine __instance,
            WindmillCraftingMasterData masterData,
            WindmillCraftingManager.CraftingData craftData)
        {
            if (__instance == null ||
                __instance.obj == null ||
                __instance.timer == null ||
                craftData is null)
            {
                return;
            }

            var id = __instance.GetInstanceID();
            Rows[id] = new PendingRow
            {
                Row = __instance,
                EndTicks = craftData.EndTimeTicks
            };
            // Try now when active, and continue refreshing after initialization.
            // Do not require a frame without ObjUpdate: some screens update every frame.
            if (__instance.obj.activeInHierarchy && __instance.timer.gameObject.activeInHierarchy)
            {
                try { Render(__instance, craftData.EndTimeTicks); }
                catch (Exception ex)
                {
                    Rows[id].Warned = true;
                    Plugin.LogSource.LogWarning($"[ETA-UI] Row {id} initial refresh failed; retrying: {ex}");
                }
            }
        }

        private static void Render(UIWindmillCraftLine __instance, long endTicks)
        {
            var dateManager = DateManager.Instance;
            if (dateManager == null)
                return;

            var now = dateManager.Now;

            if (endTicks <= now.Ticks)
                return;

            var end = new BokuMonoDateTime(endTicks);

            var source = __instance.timer;
            if (source.font == null || string.IsNullOrEmpty(source.text)) return;

            var etaText =
                WindmillEtaFormatter.Format(now, end, source);

            var timerRect = source.rectTransform;

            if (timerRect == null)
                return;

            /*
             * ETAはtimer自身の子にする。
             *
             * これにより、初回表示時のレイアウトアニメーションで
             * timerが移動してもETAが自動的についていく。
             *
             * itenNameを親にしてワールド座標を測定すると、
             * 初回だけレイアウト確定前の座標を取得することがある。
             */

            var existing = timerRect.Find(EtaObjectName);

            TextMeshProUGUI eta;

            if (existing != null)
            {
                eta = existing.GetComponent<TextMeshProUGUI>();
            }
            else
            {
                var obj = new GameObject(EtaObjectName);

                obj.SetActive(false);

                eta = obj.AddComponent<TextMeshProUGUI>();

                obj.transform.SetParent(timerRect, false);

                obj.layer = source.gameObject.layer;
            }

            if (eta == null)
                return;

            // timerの描画文字の右端を、ETA枠の右端にする。
            // timerの子なので、初回表示時の移動にも追従する。
            source.ForceMeshUpdate();
            if (source.textInfo.characterCount == 0 || source.textBounds.size.x <= 0f) return;
            var textRight = source.textBounds.max.x;
            if (float.IsNaN(textRight) || float.IsInfinity(textRight)) return;
            var rect = eta.rectTransform;
            rect.anchorMin = new Vector2(1f, 0f);
            rect.anchorMax = new Vector2(1f, 0f);
            rect.pivot = new Vector2(1f, 1f);
            rect.anchoredPosition = new Vector2(
                textRight - timerRect.rect.xMax,
                6f);
            rect.localScale = Vector3.one;

            /*
             * 既存の残り時間表示と同じ見た目を使用する。
             */
            eta.font = source.font;
            eta.fontSharedMaterial = source.fontSharedMaterial;
            eta.fontStyle = source.fontStyle;
            eta.color = source.color;

            eta.fontSize = 16f;
            eta.enableAutoSizing = false;
            eta.enableWordWrapping = false;
            eta.overflowMode = TextOverflowModes.Overflow;
            eta.alignment = TextAlignmentOptions.MidlineRight;
            eta.margin = Vector4.zero;
            eta.raycastTarget = false;

            eta.text = "→ " + etaText;
            // 右端を固定し、長い日時は左へ伸びるだけの幅を用意する。
            var width = Mathf.Max(timerRect.rect.width,
                eta.GetPreferredValues(eta.text, float.PositiveInfinity, float.PositiveInfinity).x);
            rect.sizeDelta = new Vector2(width, 22f);

            eta.gameObject.SetActive(true);
        }
    }
}