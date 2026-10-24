using ArchipelagoClient;
using System;
using System.Collections.Generic;
using System.Linq;
using ArchipelagoClient.Data;
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
                    if (!AP.I.unlocked_chars.Any(s => s.Name.Contains(__instance.spCtrl.heroCampaignList[index].nameString)))
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

    [HarmonyPatch(typeof(HeroAltButton))]
    [HarmonyPatch("Set")]
    class HeroKitSelectPatch
    {
        // Essentially reimplements HeroAltButton::Set
        private static bool Prefix(HeroAltButton __instance, ref HeroButton theHeroButton, ref BeingObject theHeroObj,
            ref HeroSelectCtrl heroSelectCtrl)
        {
           
            __instance.currentHeroButton = theHeroButton;
            __instance.heroCtrl = heroSelectCtrl;
            __instance.heroObj = theHeroObj;
            __instance.back = (UIButton) theHeroButton;
            // Look at ap-unlocked kits rather than save data
            if (!AP.I.unlocked_chars.Contains(HeroKitLookup.OSFEBeingIdToHeroKit[__instance.heroObj.beingID]))
            {
                __instance.tmpText.color = new Color(0.5f, 0.5f, 0.5f);
                __instance.tmpText.text = "??????";
                __instance.locked = true;
            }
            else
            {
                __instance.heroCtrl.unlockedHeroAltButtons.Add(__instance);
                __instance.locked = false;
                __instance.tmpText.color = Color.white;
                __instance.tmpText.text = __instance.heroObj.title;
                __instance.lockImage.alpha = 0.0f;
            }

            return false; // skip the original method
        }
    }
}