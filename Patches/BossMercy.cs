using System;
using ArchipelagoClient;
using HarmonyLib;
using UnityEngine;

public class BossMercy
{
    [HarmonyPatch(typeof(Boss))]
    [HarmonyPatch("Spare")]
    public class SparePatch
    {
        public static void Prefix(Boss __instance)
        {
            string name;
            if (__instance is BossTerraDark)
            {
                AP.I.WonRun(Ending.PacifistTrue);
                name = "Terrable";
            }
            else
            {
                name = __instance.bossID.Substring(4); // ID is prefixed with "Boss"
            }

            //Debug.Log("Spared " + name);
            ArchiNet.I.SendLocation($"[{AP.I.current_char.ToString()}] Spare {name}");
        }
    }
    
    [HarmonyPatch(typeof(Boss))]
    [HarmonyPatch("Executed")]
    public class ExecutedPatch
    {
        public static void Prefix(Boss __instance)
        {
            if (__instance is BossSerif serif)
            {
                AP.I.WonRun(Ending.Genocide);
            }

            if (__instance is BossTerraDark)
            {
                AP.I.WonRun(Ending.PacifistFalse);
            }
            string name = __instance.bossID.Substring(4); // ID is prefixed with "Boss"
            //Debug.Log("Executed " + name);
            ArchiNet.I.SendLocation($"[{AP.I.current_char.ToString()}] Kill {name}");
        }
    }
    
    [HarmonyPatch(typeof(Boss))]
    [HarmonyPatch("StartDeath")]
    public class NeutralVictoryPatch
    {
        private static void Prefix(Boss __instance)
        {
            if (__instance is BossGate gate)
            {
                AP.I.WonRun(Ending.Neutral);
                ArchiNet.I.SendLocation($"[{AP.I.current_char.ToString()}] Kill Gate");
            }
        }
    }
}