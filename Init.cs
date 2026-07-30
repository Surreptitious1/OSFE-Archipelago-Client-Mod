using HarmonyLib;
using UnityEngine;

namespace ArchipelagoClient
{
    [HarmonyPatch(typeof(S))]
    [HarmonyPatch("Awake")]
    public class Init
    {
        static void Prepare()
        {
            Debug.Log("Archi Installed");
            //TODO: duplicate install prevention
        }
    }
    
    
}