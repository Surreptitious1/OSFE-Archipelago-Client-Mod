using System;
using System.Collections.Generic;
using System.Linq;
using Archipelago.MultiClient.Net.Models;
using ArchipelagoClient.Data;
using Sirenix.Utilities;
using UnityEngine;

namespace ArchipelagoClient {
    /**
     * https://csharpindepth.com/articles/Singleton
     */
    public class AP
    {
        // Items
        public List<HeroKit> unlocked_chars = new List<HeroKit>();
        public List<String> unlocked_spells = new List<string>();
        public List<String> unlocked_artifacts = new List<string>();
        
        // Locations
        public List<String> boss_spares = new List<string>();
        public List<String> boss_kills = new List<string>();
        
        // Goal
        public List<HeroKit> pacifist_winners = new List<HeroKit>();
        public List<HeroKit> neutral_winners = new List<HeroKit>();
        public List<HeroKit> genocide_winners = new List<HeroKit>();

        private Dictionary<string, object> slotData = null;
        public Dictionary<HeroKit, List<string>> custom_spells = new Dictionary<HeroKit, List<string>>();
        public Dictionary<HeroKit, List<string>> custom_artifacts = new Dictionary<HeroKit, List<string>>();
        
        public Dictionary<string, SpellObject> original_spells = new Dictionary<string, SpellObject>();
        public Dictionary<string, ArtifactObject> original_artifacts = new Dictionary<string, ArtifactObject>();

        public HeroKit? current_char = null;
        public bool in_run = false; // Refers to OSFE run, not an Archipelago run
        
        private static readonly object initlock = new object();
        private static AP _AP = null;
         
        private AP()
        {
            // These spells are always unlocked, since they don't show up as normal combat rewards
            unlocked_spells.Add("SolarBeam");
            unlocked_spells.Add("Jam");
            unlocked_spells.Add("Yami");
        }

        public static AP I
        {
            get
            {
                lock (initlock)
                {
                    if (_AP == null)
                    {
                        _AP = new AP();
                    } 
                    return _AP;
                }
            }
        }

        public bool SpellUnlocked(string spell)
        {
            return unlocked_spells.Contains(spell);
        }

        public bool ArtifactUnlocked(string art)
        {
            return unlocked_artifacts.Contains(art);
        }

        public void StartRun(RunCtrl runCtrl)
        {
            this.current_char = HeroKitLookup.OSFEBeingIdToHeroKit[runCtrl.ctrl.currentHeroObj.beingID];
            this.in_run = true;
        }

        public void EndRun()
        {
            this.in_run = false;
        }

        public void SetSlotData(Dictionary<string, object> slotData)
        {
            this.slotData = slotData;

            // TODO: this string parsing feels like a horrible hack that shouldn't exist
            var characters = slotData["characters"].ToString().Trim('[', ']');
            foreach (string character_raw in characters.Split(','))
            {
                string character = character_raw.Trim().Trim('"');
                HeroKit kit = HeroKit.FromString(character);
                
                string spells_raw = slotData[$"{kit.ToString()}_starting_spells"].ToString().Trim('[',']');
                List<string> spells = new List<string>();
                foreach (var spell_raw in spells_raw.Split(','))
                {
                    string spell = spell_raw.Trim().Trim('"');
                    spells.Add(SpellLookup.APSpellToOSFESpell[$"Spell Card: {spell}"]);
                }
                custom_spells.Add(kit, spells);
                
                string arts_raw = slotData[$"{kit.ToString()}_starting_artifacts"].ToString().Trim('[',']');
                List<string> arts = new List<string>();
                foreach (var art_raw in arts_raw.Split(','))
                {
                    string art = art_raw.Trim().Trim('"');
                    arts.Add(ArtifactLookup.APArtToOSFEArt[$"Artifact Unlock: {art}"]);
                }
                custom_artifacts.Add(kit, arts);
            }
            
            foreach (var obj in slotData.Keys)
            {
                Debug.Log($"{obj}: {slotData[obj]}");
            }
        }

        public void ReceiveItem(ItemInfo item)
        {
            switch (item.ItemDisplayName.Split(':')[0].Trim())
            {
                case "Hero Unlock":
                    UnlockHero(item);
                    return;
                case "Spell Card":
                    UnlockCard(item);
                    return;
                case "Brand Unlock":
                    UnlockBrand(item);
                    return;
                case "Brand Rarity Unlock":
                    UnlockBrandRarity(item);
                    return;
                //TODO: hell pass
                case "Artifact Unlock":
                    UnlockArtifact(item);
                    return;
                case "Artifact Rarity Unlock":
                    UnlockArtifactRarity(item);
                    return;
                default:
                    Debug.Log("unknown item: " + item.ItemDisplayName);
                    break;
            }
            // TODO: filler
        }

        private void UnlockHero(ItemInfo item)
        {
            String hero_str = item.ItemDisplayName.Split(':')[1].Trim();
            unlocked_chars.Add(HeroKit.FromString(hero_str));
        }

        public void FixupSpellLists()
        {
            S.I.itemMan.playerFullSpellList = S.I.itemMan.spellDictionary.Values.Where<SpellObject>((Func<SpellObject, bool>) (t => t.tags.Contains(Tag.Player))).ToList<SpellObject>();
            S.I.itemMan.playerSpellList = S.I.itemMan.playerFullSpellList.ToList(); // skip the unlock level filter
            S.I.itemMan.unobtainedPlayerSpellList = new List<SpellObject>((IEnumerable<SpellObject>) S.I.itemMan.playerSpellList);
        }

        public void FixupArtLists()
        {
             S.I.itemMan.baseArtList = S.I.itemMan.artDictionary.Values.Where<ArtifactObject>((Func<ArtifactObject, bool>) (t => t.tags.Contains(Tag.Base))).ToList<ArtifactObject>();
             S.I.itemMan.playerFullArtList = S.I.itemMan.artDictionary.Values.Where<ArtifactObject>((Func<ArtifactObject, bool>) (t => t.tags.Contains(Tag.Player) && !t.tags.Contains(Tag.Cpu) && !t.tags.Contains(Tag.Starting))).ToList<ArtifactObject>();
             S.I.itemMan.nonBaseArtList = S.I.itemMan.playerFullArtList.Where<ArtifactObject>((Func<ArtifactObject, bool>) (t => !t.tags.Contains(Tag.Base))).ToList<ArtifactObject>(); // Again, ignore unlock level
             S.I.itemMan.buffList = S.I.itemMan.artDictionary.Values.Where<ArtifactObject>((Func<ArtifactObject, bool>) (t => t.tags.Contains(Tag.Buff))).ToList<ArtifactObject>();
             S.I.itemMan.loopMutationList = S.I.itemMan.artDictionary.Values.Where<ArtifactObject>((Func<ArtifactObject, bool>) (t => t.tags.Contains(Tag.LoopMutation))).ToList<ArtifactObject>();
        }

        // MUST be called from a hook (Unity "main thread"), NOT from the multiworld server's callback
        public void FixupCurrentDeck()
        {
            if (in_run)
            {
                foreach (var disk in S.I.deCtrl.duelDisks)
                {
                    List<ListCard> spells = new List<ListCard>();
                    // Don't mutate the deck before getting all its values
                    foreach (var spell in disk.deck)
                    {
                        spells.Add(spell);
                    }
                    int i = 0;
                    foreach (var owned_spell in spells) {
                        Debug.Log("FSL: " + owned_spell.spellObj.itemID);
                        S.I.itemMan.unobtainedPlayerSpellList.Remove(owned_spell.spellObj);
                        // TODO: keep an eye on this if performance gets problematic
                        if (owned_spell.spellObj.nameString.StartsWith("[LOCKED]") && 
                            unlocked_spells.Contains(owned_spell.spellObj.itemID))
                        {
                            S.I.deCtrl.RemoveCardFromDeck(owned_spell, false);
                            S.I.deCtrl.CreateSpellCard(original_spells[owned_spell.spellObj.itemID],
                                S.I.deCtrl.deckScreen.deckGrid, i, disk);
                        }

                        i++;
                    }
                }
            }
        }

        private void UnlockSpell(String spell)
        {
            unlocked_spells.Add(spell);
            S.I.itemMan.spellDictionary[spell] = original_spells[spell];
        }

        private void UnlockCard(ItemInfo item)
        {
            String osfe_spell = SpellLookup.APSpellToOSFESpell[item.ItemDisplayName];
            UnlockSpell(osfe_spell);
            FixupSpellLists();
        }

        private void UnlockBrand(ItemInfo item)
        {
            List<String> ap_spells = SpellLookup.APBrandToAPSpells[item.ItemDisplayName];
            foreach (String ap_spell in ap_spells)
            {
                String osfe_spell = SpellLookup.APSpellToOSFESpell[ap_spell];
                UnlockSpell(osfe_spell);
            }
            FixupSpellLists();
        }

        private void UnlockBrandRarity(ItemInfo item)
        {
            List<String> ap_spells = SpellLookup.APBrandRarityToAPSpells[item.ItemDisplayName];
            foreach (String ap_spell in ap_spells)
            {
                String osfe_spell = SpellLookup.APSpellToOSFESpell[ap_spell];
                UnlockSpell(osfe_spell);
            }
            FixupSpellLists();
        }

        private void _UnlockArtifact(String art)
        {
            unlocked_artifacts.Add(art);
            S.I.itemMan.artDictionary[art] = original_artifacts[art]; 
        }
        
        private void UnlockArtifact(ItemInfo art)
        {
            String osfe_art = ArtifactLookup.APArtToOSFEArt[art.ItemDisplayName];
            _UnlockArtifact(osfe_art);
            FixupArtLists();
        }

        private void UnlockArtifactRarity(ItemInfo item)
        {
            List<string> arts = ArtifactLookup.APArtRarityToOSFEArt[item.ItemDisplayName];
            foreach (var art in arts)
            {
                _UnlockArtifact(art);
            }
            FixupArtLists();
        }

        public bool IsGoaled()
        {
            if (this.slotData == null)
            {
                return false;
            }

            int character_wins = int.Parse(slotData["character_wins"].ToString());
            int neutral_wins = int.Parse(slotData["neutral_wins"].ToString());
            int pacifist_wins = int.Parse(slotData["pacifist_wins"].ToString());
            int genocide_wins = int.Parse(slotData["genocide_wins"].ToString());

            HashSet<HeroKit> unique_winners = new HashSet<HeroKit>();
            unique_winners.AddRange(neutral_winners);
            unique_winners.AddRange(pacifist_winners);
            unique_winners.AddRange(genocide_winners);
            
            Debug.Log($"IsGoaled:\nNeutral[{this.neutral_winners.Count}/{neutral_wins}\nPacifist[{this.pacifist_winners.Count}/{pacifist_wins}]\nGenocide[{this.genocide_winners.Count}/{genocide_wins}]\nCharacter[{unique_winners.Count}/{character_wins}]");

            return this.neutral_winners.Count >= neutral_wins &&
                   this.pacifist_winners.Count >= pacifist_wins &&
                   this.genocide_winners.Count >= genocide_wins &&
                   unique_winners.Count >= character_wins;
        }
    }
}