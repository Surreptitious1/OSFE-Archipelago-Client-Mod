using System.Collections.Generic;
using ArchipelagoClient.Data;
using HarmonyLib;
using UnityEngine;

namespace ArchipelagoClient.Patches
{
    public class CustomItems
    {
        // Save off an unmodified original copy of each base spell
        private static void SaveOriginalSpells(ItemManager __instance)
        {
            foreach (var spell in SpellLookup.APSpellToOSFESpell.Values)
            {
                SpellObject copy = __instance.spellDictionary[spell].Clone();
                AP.I.original_spells[spell] = copy;
                //Debug.Log(copy.nameString);
            }
        }

        private static void SaveOriginalArtifacts(ItemManager __instance)
        {
            foreach (var art in ArtifactLookup.APArtToOSFEArt.Values)
            {
                ArtifactObject copy = __instance.artDictionary[art].Clone();
                AP.I.original_artifacts[art] = copy;
                //Debug.Log(copy.nameString);
            }
        }

        private static void LockUnavailableSpells(ItemManager __instance)
        {
            foreach (var spell in SpellLookup.APSpellToOSFESpell.Values)
            {
                if (AP.I.SpellUnlocked(spell)) continue;
                
                // Use jam for behavior
                SpellObject jam = __instance.spellDictionary["Jam"];
                SpellObject original = AP.I.original_spells[spell];
                SpellObject locked_copy = original.Clone();

                // Stolen from ItemObject::SetItemAtts...
                locked_copy.sprite = jam.sprite;
                locked_copy.effectTags = new List<Effect>((IEnumerable<Effect>) jam.effectTags);
                locked_copy.triggerTags = new List<FTrigger>((IEnumerable<FTrigger>) jam.triggerTags);
                locked_copy.efApps = new List<EffectApp>();
                foreach (EffectApp efApp in jam.efApps)
                    locked_copy.efApps.Add(efApp.Clone());
                locked_copy.paramDictionary = new Dictionary<string, string>((IDictionary<string, string>) jam.paramDictionary); 
                //...and protected SpellObject constructor
                locked_copy.mana = jam.mana;
                locked_copy.manaType = jam.manaType;
                locked_copy.animObj = jam.animObj;
                locked_copy.animCast = jam.animCast;
                locked_copy.animBlast = jam.animBlast;
                locked_copy.animShot = jam.animShot;
                locked_copy.animHit = jam.animHit;
                locked_copy.animWarning = jam.animWarning;
                locked_copy.introSound = jam.introSound;
                locked_copy.castSound = jam.castSound;
                locked_copy.shotSound = jam.shotSound;
                locked_copy.hitSound = jam.hitSound;
                locked_copy.warningSound = jam.warningSound;
                locked_copy.tileApps = new List<TileApp>((IEnumerable<TileApp>)jam.tileApps);
                locked_copy.damage = jam.damage;
                locked_copy.numTiles = jam.numTiles;
                locked_copy.numShots = jam.numShots;
                locked_copy.numShotsType = jam.numShotsType;
                locked_copy.timeBetweenShots = jam.timeBetweenShots;
                locked_copy.shotVelocity = jam.shotVelocity;
                locked_copy.shotVelocityY = jam.shotVelocityY;
                locked_copy.shotDuration = jam.shotDuration;
                locked_copy.shotDelay = jam.shotDelay;
                locked_copy.castDelay = jam.castDelay;
                locked_copy.castDuration = jam.castDuration;
                locked_copy.blastDuration = jam.blastDuration;
                locked_copy.warningDuration = jam.warningDuration;
                locked_copy.cooldown = jam.cooldown;
                locked_copy.anchor = jam.anchor;
                locked_copy.channel = jam.channel;
                locked_copy.interrupt = jam.interrupt;
                locked_copy.backfire = jam.backfire;
                locked_copy.lineTracer = jam.lineTracer;
                locked_copy.destroyOnDeath = jam.destroyOnDeath;
                locked_copy.introDelay = jam.introDelay;
                locked_copy.fireAnim = jam.fireAnim;
                locked_copy.fireAnimLate = jam.fireAnimLate;
                locked_copy.gunPointSetting = jam.gunPointSetting;
                locked_copy.yVariance = jam.yVariance;
                locked_copy.consume = jam.consume;
                locked_copy.flow = jam.flow;
                locked_copy.trinityCast = jam.trinityCast;
                locked_copy.spellActions = new List<string>((IEnumerable<string>)jam.spellActions);
                locked_copy.timeOutActions = new List<string>((IEnumerable<string>)jam.timeOutActions);
                locked_copy.statusApps = new List<StatusApp>((IEnumerable<StatusApp>)jam.statusApps);
                locked_copy.enhancements = new List<Enhancement>((IEnumerable<Enhancement>)jam.enhancements);
                locked_copy.damageType = jam.damageType;
                locked_copy.pierceDefense = jam.pierceDefense;
                locked_copy.pierceShield = jam.pierceShield;
                locked_copy.hitAllies = jam.hitAllies;
                locked_copy.hitEnemies = jam.hitEnemies;
                locked_copy.hitSelf = jam.hitSelf;
                locked_copy.hitStructures = jam.hitStructures;
                locked_copy.destroyOnHit = jam.destroyOnHit;
                locked_copy.onHitTriggerArts = jam.onHitTriggerArts;
                locked_copy.effectLayer = jam.effectLayer;
                locked_copy.dashDistance = jam.dashDistance;
                locked_copy.dashHeight = jam.dashHeight;
                locked_copy.slashLocation = jam.slashLocation;
                locked_copy.hitboxWidth = jam.hitboxWidth;
                locked_copy.hitboxHeight = jam.hitboxHeight;
                locked_copy.hitboxOffset = jam.hitboxOffset;
                locked_copy.spawnOffset = jam.spawnOffset;
                locked_copy.recoveryTime = jam.recoveryTime;
                locked_copy.faceVelocity = jam.faceVelocity;
                locked_copy.bending = jam.bending;
                locked_copy.timeToTravel = jam.timeToTravel;
                locked_copy.arcType = jam.arcType;
                locked_copy.fireLoop = jam.fireLoop;
                locked_copy.tempDamage = jam.tempDamage;
                locked_copy.permDamage = jam.permDamage;
                locked_copy.spellObj = locked_copy;
                    
                locked_copy.nameString = "[LOCKED] " +  original.nameString;
                locked_copy.description = "Does nothing...? " + original.description;
                locked_copy.tags = original.tags;
                __instance.spellDictionary[spell] = locked_copy;
            }
        }

        private static void LockUnavailableArtifacts(ItemManager __instance)
        {
            foreach (var art in ArtifactLookup.APArtToOSFEArt.Values)
            {
                if (AP.I.ArtifactUnlocked(art)) continue;
                
                // Use XD-40 as template
                ArtifactObject xd40 = __instance.artDictionary["XD40"];
                ArtifactObject original = AP.I.original_artifacts[art];
                ArtifactObject locked_copy = original.Clone();
                    
                // Stolen from ItemObject::SetItemAtts...
                locked_copy.sprite = xd40.sprite;
                locked_copy.effectTags = new List<Effect>((IEnumerable<Effect>) xd40.effectTags);
                locked_copy.triggerTags = new List<FTrigger>((IEnumerable<FTrigger>) xd40.triggerTags);
                locked_copy.efApps = new List<EffectApp>();
                foreach (EffectApp efApp in xd40.efApps)
                    locked_copy.efApps.Add(efApp.Clone());
                locked_copy.paramDictionary = new Dictionary<string, string>((IDictionary<string, string>) xd40.paramDictionary); 
                //...and protected ArtifactObject constructor
                locked_copy.amount = xd40.amount;
                locked_copy.maxHP = xd40.maxHP;
                locked_copy.maxMana = xd40.maxMana;
                locked_copy.startingManaChange = xd40.startingManaChange;
                locked_copy.manaRegen = xd40.manaRegen;
                locked_copy.atkDmg = xd40.atkDmg;
                locked_copy.spellPower = xd40.spellPower;
                locked_copy.defense = xd40.defense;
                locked_copy.invulLength = xd40.invulLength;
                locked_copy.shuffleTime = xd40.shuffleTime;
                locked_copy.dropMultiplierAdd = xd40.dropMultiplierAdd;
                locked_copy.lootDropsAdd = xd40.lootDropsAdd;
                locked_copy.spell = "";
                locked_copy.artObj = locked_copy;
                    
                locked_copy.nameString = "[LOCKED] " +  original.nameString;
                locked_copy.description = "Does nothing...? " + original.description;
                locked_copy.tags = original.tags;
                __instance.artDictionary[art] = locked_copy;
            }
        }

        [HarmonyPatch(typeof(ItemManager))]
        [HarmonyPatch("ReadSpellFile")]
        private class ReadSpellFilePatch
        {
            private static void Postfix(ItemManager __instance)
            {
                CustomItems.SaveOriginalSpells(__instance);
                CustomItems.LockUnavailableSpells(__instance);
            }
        }

        [HarmonyPatch(typeof(ItemManager))]
        [HarmonyPatch("CreateArtifactObjectPrototypes")]
        private class CreateArtifactObjectPrototypesPatch
        {
            private static void Postfix(ItemManager __instance)
            {
                CustomItems.SaveOriginalArtifacts(__instance);
                CustomItems.LockUnavailableArtifacts(__instance);
            }
        }
    }
}