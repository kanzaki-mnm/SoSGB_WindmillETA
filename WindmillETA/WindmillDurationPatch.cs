using System;
using System.Reflection;
using BokuMono;
using BokuMono.Data;
using HarmonyLib;

namespace WindmillETA
{
    [HarmonyPatch]
    internal static class WindmillDurationPatch
    {
        private static MethodBase TargetMethod()
        {
            var byRef = typeof(int).MakeByRefType();
            return AccessTools.Method(typeof(WindmillCraftingMaster), "GetTime",
                new[] { typeof(int), typeof(int), typeof(NiceParts), typeof(int),
                    byRef, byRef, byRef, byRef });
        }

        private static void Postfix(int time, ref int day, ref int hour, ref int minute)
        {
            // Preserve the original minute-precision ETA. Do not apply modifiers again.
            WindmillDurationCapture.Record(time, day, hour, minute);
        }
    }
}
