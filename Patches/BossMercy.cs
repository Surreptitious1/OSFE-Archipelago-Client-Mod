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
            string name = __instance.bossID.Substring(4); // ID is prefixed with "Boss"
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
            string name = __instance.bossID.Substring(4); // ID is prefixed with "Boss"
            //Debug.Log("Executed " + name);
            ArchiNet.I.SendLocation($"[{AP.I.current_char.ToString()}] Kill {name}");
        }
    }
}