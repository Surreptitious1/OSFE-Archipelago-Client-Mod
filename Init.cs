using ArchipelagoClient.Data;
using ArchipelagoClient.Patches;
using HarmonyLib;
using Kittehface.Framework20;
using UnityEngine;

namespace ArchipelagoClient
{
    [HarmonyPatch(typeof(S))]
    [HarmonyPatch("Awake")]
    public class Init
    {
        static void Prepare()
        {
            foreach (var obj in S.I.spCtrl.heroCampaignList)
            {
                //Debug.Log(obj.nameString);
                foreach (var id in S.I.heCtrl.HeroAltList(obj.nameString))
                {
                    Debug.Log(id.beingID);
                }
            }
            SaveDataCtrl.Set<int>("UnlockLevel", 50);
            S.I.unCtrl.UnlockAll();
            //TODO: duplicate install prevention
            //TODO: dllimport MultiClient
            Debug.Log("Archi Installed");
        }

    }
    
    
}