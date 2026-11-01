

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

            if (!AP.I.did_run_init)
            {
                Debug.Log("player");
                S.I.batCtrl.currentPlayer.health.SetMax(S.I.batCtrl.currentPlayer.health.max + AP.I.bonus_health);
                Debug.Log("sera");
                S.I.shopCtrl.ModifySera(AP.I.bonus_money);
                Debug.Log("uppy");
                S.I.shopCtrl.ModifyUpgraderCount(AP.I.bonus_upgrader);
                Debug.Log("rem");
                S.I.shopCtrl.ModifyRemovalCount(AP.I.bonus_remover);
                AP.I.did_run_init = true;
            }
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