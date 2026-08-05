

using ArchipelagoClient;
using HarmonyLib;

public class RunTracking
{
    [HarmonyPatch(typeof(RunCtrl))]
    [HarmonyPatch("StartCampaign")]
    public class StartCampaignPatch
    {
        public static void Prefix(RunCtrl __instance)
        {
            AP.I.StartRun(__instance);
        }
    }
    
    [HarmonyPatch(typeof(RunCtrl))]
    [HarmonyPatch("GoToNextZone")]
    public class GoToNextZonePatch
    {
        private static void Prefix(RunCtrl __instance)
        {
            // TODO: send location pings to AP state tracking
            //__instance.currentRun.zoneIndex
            //__instance.currentRun.worldTierNum
        }
    }
}