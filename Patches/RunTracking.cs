

using ArchipelagoClient;
using HarmonyLib;
using UnityEngine;

public class RunTracking
{
    [HarmonyPatch(typeof(RunCtrl))]
    [HarmonyPatch("StartCampaign")]
    public class StartCampaignPatch
    {
        // Postfix since StartCampaign may call DeleteRun
        public static void Postfix(RunCtrl __instance)
        {
            AP.I.StartRun(__instance.ctrl.currentHeroObj.beingID);
        }
    }

    [HarmonyPatch(typeof(RunCtrl))]
    [HarmonyPatch("CreateRunFromSave")]
    public class LoadRunPatch
    {
        public static void Postfix(RunCtrl __instance)
        {
            AP.I.StartRun(__instance.ctrl.currentHeroObj.beingID);
        }
    }
    
    [HarmonyPatch(typeof(RunCtrl))]
    [HarmonyPatch("DeleteRun")]
    public class DeleteRunPatch
    {
        private static void Prefix(RunCtrl __instance)
        {
            AP.I.EndRun();
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
            AP.I.FixupCurrentDeck();
            AP.I.FixupCurrentArts();
            AP.I.FixupCurrentHellpasses();
        }
    }

    [HarmonyPatch(typeof(BossSerif))]
    [HarmonyPatch("GateExecution")]
    public class NeutralVictoryPatch
    {
        private static void Prefix(BossSerif __instance)
        {
            AP.I.WonRun(Ending.Neutral);
        }
    }

    [HarmonyPatch(typeof(BC))]
    [HarmonyPatch("Victory")]
    public class VictoryPatch
    {
        private static void Prefix(BC __instance, ref Ending endingType)
        {
            AP.I.WonRun(endingType);
        }
    }
}