using BokuMono;
using HarmonyLib;

namespace WindmillETA
{
    [HarmonyPatch(typeof(UIManager), "Update")]
    internal static class WindmillCraftLineRefreshPatch
    {
        private static void Postfix()
        {
            WindmillCraftLinePatch.RefreshVisibleRows();
        }
    }
}
