using System.Runtime.CompilerServices;
using ArchipelagoClient;
using HarmonyLib;
using UnityEngine;

public class HellPassLock
{
    [HarmonyPatch(typeof(HellPassListCard))]
    [HarmonyPatch("SetActivated")]
    public class HellPassBtnDisable
    {
        private static void Prefix(HellPassListCard __instance, ref bool setToActivated)
        {
            setToActivated = AP.I.enabled_hellpasses.Contains(__instance.hellPassNum);
        }
    }

    /*
    [HarmonyPatch(typeof(HellPassButton))]
    [HarmonyPatch("SetActiveHellPasses")]
    public class HellPassSetPatch
    {
        private static void Prefix(HellPassButton __instance)
        {
            for (int i = 0; i < S.I.itemMan.hellPasses.Count; i++)
            {
                __instance.hellPassListCards[i].SetActivated(AP.I.enabled_hellpasses.Contains(i), false);
            }
        }
    }
    */
}