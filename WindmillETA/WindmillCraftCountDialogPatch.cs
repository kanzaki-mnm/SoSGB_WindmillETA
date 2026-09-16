using BokuMono;
using HarmonyLib;
using TMPro;
using UnityEngine;

namespace WindmillETA
{
    /// <summary>参考時間の下に、横幅全体を使う完了予定欄を表示します。</summary>
    [HarmonyPatch(typeof(UIWindmillCraftCountDialog), "OnUpdate")]
    internal static class WindmillCraftCountDialogPatch
    {
        private const string EtaObjectName = "WindmillETA_Completion";
        private static bool warnedMissingDuration;

        private static void Prefix(UIWindmillCraftCountDialog __instance, out WindmillDurationCapture __state)
        {
            __state = new WindmillDurationCapture(__instance.time);
            // Never leave a previous recipe's ETA visible if this update cannot calculate it.
            var existing = __instance.timeText?.transform.parent?.Find(EtaObjectName);
            if (existing != null) existing.gameObject.SetActive(false);
        }

        private static void Finalizer(WindmillDurationCapture __state)
        {
            __state?.Dispose();
        }

        private static void Postfix(UIWindmillCraftCountDialog __instance, WindmillDurationCapture __state)
        {
            if (__instance is null || __instance.timeText is null) return;
            var durationMinutes = __state?.Minutes;
            if (!durationMinutes.HasValue)
            {
                if (!warnedMissingDuration)
                {
                    Plugin.LogSource.LogWarning("No native duration captured for the count dialog. Skipping ETA; please report this with LogOutput.log.");
                    warnedMissingDuration = true;
                }
                return;
            }
            var dateManager = DateManager.Instance;
            if (dateManager == null) return;
            var source = __instance.timeText;
            var now = dateManager.Now;
            var end = now.AddMinutes(durationMinutes.Value);
            var etaText = WindmillEtaFormatter.Format(now, end, source);
            var eta = GetOrCreateEta(source);
            if (eta == null) return;

            // Follow language-specific fonts and colors, without cloning game scripts
            // that could overwrite the text or shrink it on subsequent UI updates.
            eta.font = source.font;
            eta.fontSharedMaterial = source.fontSharedMaterial;
            eta.fontStyle = source.fontStyle;
            eta.color = source.color;
            eta.fontSize = 24f;
            eta.enableAutoSizing = false;
            eta.enableWordWrapping = false;
            eta.overflowMode = TextOverflowModes.Overflow;
            eta.alignment = TextAlignmentOptions.MidlineRight;
            eta.margin = Vector4.zero;
            eta.raycastTarget = false;
            eta.text = "→ " + etaText;
            eta.gameObject.SetActive(true);
        }

        private static TextMeshProUGUI GetOrCreateEta(LocalizedTextMeshPro source)
        {
            var timeArea = source.transform.parent;
            if (timeArea == null) return null;
            var existing = timeArea.Find(EtaObjectName);
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
                obj.transform.SetParent(timeArea, false);
                obj.layer = source.gameObject.layer;
            }
            if (eta == null) return null;

            // Dedicated row directly below the native one-line process-time area.
            // Stretch across that whole area, leaving 12 units of padding each side.
            // Parenting to the native area also handles dialog destruction/recreation.
            var rect = eta.rectTransform;
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(1f, 0f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.anchoredPosition = new Vector2(0f, -6f);
            rect.sizeDelta = new Vector2(-24f, 44f);
            rect.localScale = Vector3.one;
            return eta;
        }
    }
}
