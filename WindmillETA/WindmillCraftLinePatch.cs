using BokuMono;
using BokuMono.Data;
using HarmonyLib;

namespace WindmillETA
{
    /// <summary>
    /// 加工中アイテム一覧に完了予定時刻の表示を追加します。
    /// </summary>
    [HarmonyPatch(
        typeof(UIWindmillCraftLine),
        "ObjUpdate",
        new[]
        {
            typeof(WindmillCraftingMasterData),
            typeof(WindmillCraftingManager.CraftingData)
        }
    )]
    public static class WindmillCraftLinePatch
    {
        public static void Postfix(
            UIWindmillCraftLine __instance,
            WindmillCraftingMasterData masterData,
            WindmillCraftingManager.CraftingData craftData)
        {
            // 空き枠などは処理しない
            if (__instance == null || __instance.timer == null || craftData is null)
            {
                return;
            }

            // 見た目調整
            SetupDisplay(__instance);

            // 完了予定時刻を計算
            var now = DateManager.Instance.Now;
            var end = new BokuMonoDateTime(craftData.EndTimeTicks);
            var etaText = WindmillEtaFormatter.Format(now, end);

            // 表示
            __instance.timer.text += "\n" + etaText;
        }

        private static void SetupDisplay(UIWindmillCraftLine instance)
        {
            SetupLabels(instance);
            SetupTimer(instance);
        }

        private static void SetupLabels(UIWindmillCraftLine instance)
        {
            var texts =
                instance.obj.GetComponentsInChildren<LocalizedTextMeshPro>(true);

            foreach (var text in texts)
            {
                if (text.gameObject.name == "Text" &&
                    text.transform.parent?.name == "Time")
                {
                    text.text = "あと約\n完了予定";
                    text.lineSpacing = -12f;
                    text.fontSize = 22f;
                    return;
                }
            }
        }

        private static void SetupTimer(UIWindmillCraftLine instance)
        {
            instance.timer.lineSpacing = -12f;
            instance.timer.fontSize = 22f;
        }
    }
}