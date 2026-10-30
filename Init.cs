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
            /*
            var spells = (SpellLookup.APBrandRarityToAPSpells["Brand Rarity Unlock: Common Anima"]);
            foreach (var spell in spells)
            {
                Debug.Log("common: " + spell);
            }
            spells = SpellLookup.APBrandRarityToAPSpells["Brand Rarity Unlock: Rare Anima"];
            foreach (var spell in spells)
            {
                Debug.Log("rare " + spell);
            }
            spells = SpellLookup.APBrandRarityToAPSpells["Brand Rarity Unlock: Epic Anima"];
            foreach (var spell in spells)
            {
                Debug.Log("epic " + spell);
            }
            spells = SpellLookup.APBrandRarityToAPSpells["Brand Rarity Unlock: Legendary Anima"];
            foreach (var spell in spells)
            {
                Debug.Log("legendary " + spell);
            }
            spells = SpellLookup.APBrandRarityToAPSpells["Brand Rarity Unlock: Calamity Anima"];
            foreach (var spell in spells)
            {
                Debug.Log("calamity " + spell);
            }
            */
            
            SaveDataCtrl.Set<int>("UnlockLevel", 50);
            S.I.unCtrl.UnlockAll();
            //TODO: duplicate install prevention
            //TODO: dllimport MultiClient
            Debug.Log("Archi Installed");
        }

    }
    
    
}