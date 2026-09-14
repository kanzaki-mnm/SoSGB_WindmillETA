using BokuMono;
using HarmonyLib;
using UnityEngine;

namespace WindmillETA
{
    /// <summary>
    /// 風車の作成個数指定画面に完了予定時刻を追加します。
    /// </summary>
    [HarmonyPatch(typeof(UIWindmillCraftCountDialog), "OnUpdate")]
    public static class WindmillCraftCountDialogPatch
    {
        public static void Postfix(UIWindmillCraftCountDialog __instance)
        {
            if (__instance is null || __instance.timeText is null)
            {
                return;
            }

            // 見た目調整
            SetupDisplay(__instance);

            // 完了予定時刻を計算
            var durationMinutes = ParseDurationMinutes(__instance.timeText.text);
            var now = DateManager.Instance.Now;
            var end = now.AddMinutes(durationMinutes);

            var etaText = WindmillEtaFormatter.Format(now, end);

            // 表示
            __instance.timeText.text += "\n" + etaText;
        }

        private static void SetupDisplay(UIWindmillCraftCountDialog instance)
        {
            SetupLabels(instance);
            SetupTimeArea(instance);
        }

        private static void SetupLabels(UIWindmillCraftCountDialog instance)
        {
            var parent = instance.timeText.transform.parent;

            if (parent == null)
            {
                return;
            }

            var title = parent.Find("TitleText")?
                .GetComponent<LocalizedTextMeshPro>();

            if (title == null)
            {
                return;
            }

            title.text = "参考時間\n完了予定";

            title.lineSpacing = -8f;
            title.fontSize = 24f;
        }

        private static void SetupTimeArea(UIWindmillCraftCountDialog instance)
        {
            var timeArea =
                instance.timeText.transform.parent?.GetComponent<RectTransform>();

            if (timeArea != null)
            {
                timeArea.sizeDelta =
                    new Vector2(timeArea.sizeDelta.x, 72f);
            }

            instance.timeText.rectTransform.sizeDelta =
                new Vector2(
                    instance.timeText.rectTransform.sizeDelta.x,
                    64f
                );

            instance.timeText.lineSpacing = -8f;
            instance.timeText.fontSize = 24f;
        }

        private static int ParseDurationMinutes(string text)
        {
            int total = 0;

            var dayMatch =
                System.Text.RegularExpressions.Regex.Match(text, @"(\d+)日");

            if (dayMatch.Success)
            {
                total += int.Parse(dayMatch.Groups[1].Value) * 24 * 60;
            }

            var hourMatch =
                System.Text.RegularExpressions.Regex.Match(text, @"(\d+)時間");

            if (hourMatch.Success)
            {
                total += int.Parse(hourMatch.Groups[1].Value) * 60;
            }

            var minuteMatch =
                System.Text.RegularExpressions.Regex.Match(text, @"(\d+)分");

            if (minuteMatch.Success)
            {
                total += int.Parse(minuteMatch.Groups[1].Value);
            }

            return total;
        }
    }
}