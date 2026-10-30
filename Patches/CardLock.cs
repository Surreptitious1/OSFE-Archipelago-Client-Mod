

using System.Collections.Generic;
using ArchipelagoClient;
using ArchipelagoClient.Data;
using HarmonyLib;
using UnityEngine;

public class CardLock
{
    /* NOTE: Probably obviated by SetHeroItems patch
    // Replace locked cards in starting decks with jam
    [HarmonyPatch(typeof(SpawnCtrl))]
    [HarmonyPatch("CreateBeing")]
    class CreateBeingPatch
    {
        private static void Prefix(SpawnCtrl __instance, ref BeingObject beingObj)
        {
            if (beingObj.type == BeingType.Hero)
            {
                for (int i = 0; i < beingObj.deck.Count; i++)
                {
                    if (!AP.I.unlocked_spells.Contains(beingObj.deck[i]))
                    {
                        beingObj.deck.RemoveAt(i);
                        beingObj.deck.Insert(i, "Jam");
                    }
                }
            }
        }
    }
    */
    
    // Replace cards at character selection time
    [HarmonyPatch(typeof(HeroDisplay))]
    [HarmonyPatch("SetHeroItems")]
    class SetHeroItemsPatch
    {
        private static void Prefix(HeroDisplay __instance, ref BeingObject beingObj)
        {
            // Replace the default deck with the randomized one
            var kit = HeroKitLookup.OSFEBeingIdToHeroKit[beingObj.beingID];
            if (AP.I.custom_spells.ContainsKey(kit))
            {
                beingObj.deck.Clear();
                foreach (var spell in AP.I.custom_spells[kit])
                {
                    beingObj.deck.Add(spell);
                }
            }

            if (AP.I.custom_artifacts.ContainsKey(kit))
            {
                beingObj.artifacts.Clear();
                foreach (var art in AP.I.custom_artifacts[kit])
                {
                    beingObj.artifacts.Add(new ArtData(S.I.itemMan.artDictionary[art]));
                }
            }

            /*
            // TODO: instead of jams, replace locked spells with something custom
            for (int i = 0; i < beingObj.deck.Count; i++)
            {
                if (!AP.I.unlocked_spells.Contains(beingObj.deck[i]))
                {
                    beingObj.deck[i] = "[LOCKED] " + beingObj.deck[i];
                    Debug.Log("CardLock: " + beingObj.deck[i]);
                }
            }
            */
        }
    }

    /*
    [HarmonyPatch(typeof(ItemManager))]
    [HarmonyPatch("LoadItemData")]
    class DumpItemData
    {
        private static void Postfix(ItemManager __instance)
        {
            Debug.Log("==== Printing Artifact Dict ====");
            foreach (string key in __instance.artDictionary.Keys)
            {
                Debug.Log(key + ":" + __instance.artDictionary[key]);
            }
            
            Debug.Log("==== Printing Item Dict ====");
            foreach (string key in __instance.itemDictionary.Keys)
            {
                Debug.Log(key + ":" + __instance.itemDictionary[key]);
            }
            Debug.Log("==== Printing Pact Dict ====");
            foreach (string key in __instance.pactDictionary.Keys)
            {
                Debug.Log(key + ":" + __instance.pactDictionary[key]);
            }
            Debug.Log("==== Printing Spell Dict ====");
            foreach (string key in __instance.spellDictionary.Keys)
            {
                Debug.Log(key + ":" + __instance.spellDictionary[key]);
            }
        }
    }
    */
        
    /*
    // Replace locked cards in rewards with jam
    [HarmonyPatch(typeof(ItemManager))]
    [HarmonyPatch("GetItems")]
    class GetItemsPatch
    {
        private static void Postfix(ItemManager __instance, ref List<ItemObject> __result)
        {
            for (int i = 0; i < __result.Count; i++)
            {
                ItemObject obj =  __result[i];
                if (obj.type == ItemType.Spell && !AP.I.unlocked_spells.Contains(obj.shortName))
                {
                    __result[i] = __instance.spellDictionary["[LOCKED] " + obj.shortName];
                }
            }
        }
    }
    */
    
}

