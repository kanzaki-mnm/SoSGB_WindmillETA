using BepInEx;
using BepInEx.Unity.IL2CPP;
using BepInEx.Logging;
using HarmonyLib;

namespace WindmillETA
{
    [BepInPlugin("com.icy.windmilleta", "WindmillETA", "1.0.0")]
    public class Plugin : BasePlugin
    {
        public static ManualLogSource LogSource;

        public override void Load()
        {
            LogSource = Log;

            Log.LogInfo("WindmillETA loaded!");

            Harmony harmony = new Harmony("com.icy.windmilleta");
            harmony.PatchAll();

            Log.LogInfo("WindmillETA patches applied!");
        }
    }
}