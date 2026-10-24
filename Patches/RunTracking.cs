

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
            int zone = __instance.currentRun.zoneNum;
            int world = __instance.currentRun.worldTierNum + 1;
            int eden_id = 8 + (AP.I.current_char?.Name == "Shopkeeper" ? 1 : 0);
            if (world == eden_id)
            {
                ArchiNet.I.SendLocation($"[{AP.I.current_char.ToString()}] Eden, Zone {zone}");
            }
            else
            {
                ArchiNet.I.SendLocation($"[{AP.I.current_char.ToString()}] World {world}, Zone {zone}");
            }
        }
    }
}