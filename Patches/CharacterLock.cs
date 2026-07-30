using ArchipelagoClient;
using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using UnityEngine;


class CharacterLock
{
    [HarmonyPatch(typeof(HeroSelectCtrl))]
    [HarmonyPatch("CreateHeroButtons")]
    class CreateHeroButtonsPatch
    {
        // Essentially reimplements HeroSelectCtrl::CreateHeroButtons with logic to omit locked heroes
        private static bool Prefix(HeroSelectCtrl __instance)
        {
            {
                __instance.heroGrid.DestroyChildren();
                __instance.heroButtons.Clear();
                __instance.unlockedHeroButtons.Clear();
                for (int index = 0; index < __instance.spCtrl.heroCampaignList.Count; ++index)
                {
                    if (!AP.I.unlocked_chars.Any(s => s.Contains(__instance.spCtrl.heroCampaignList[index].nameString)))
                    {
                        // skip characters that aren't unlocked yet
                        continue;
                    }
                    HeroButton component = UnityEngine.Object.Instantiate<HeroButton>(__instance.heroBtnPrefab, __instance.transform.position + Vector3.back, __instance.transform.rotation).GetComponent<HeroButton>();
                    component.Set(__instance, __instance.spCtrl.heroCampaignList[index]);
                    __instance.heroButtons.Add(component);
                }
            }
            return false; // skip the original method
        }
    }
}