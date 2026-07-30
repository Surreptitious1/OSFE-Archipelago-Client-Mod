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
            Debug.Log("Spared " + name);
            if (!AP.I.boss_spares.Contains(name))
                AP.I.boss_spares.Add(name);
        }
    }
    
    [HarmonyPatch(typeof(Boss))]
    [HarmonyPatch("Executed")]
    public class ExecutedPatch
    {
        public static void Prefix(Boss __instance)
        {
            string name = __instance.bossID.Substring(4); // ID is prefixed with "Boss"
            Debug.Log("Executed " + name);
            if (!AP.I.boss_kills.Contains(name))
                AP.I.boss_kills.Add(name);
        }
    }
}