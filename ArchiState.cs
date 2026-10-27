using System;
using System.Collections.Generic;
using System.Linq;
using Archipelago.MultiClient.Net.Models;
using ArchipelagoClient.Data;
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
        
        // Locations
        public List<String> boss_spares = new List<string>();
        public List<String> boss_kills = new List<string>();
        
        // Goal
        public List<HeroKit> pacifist_winners = new List<HeroKit>();
        public List<HeroKit> neutral_winners = new List<HeroKit>();
        public List<HeroKit> genocide_winners = new List<HeroKit>();

        private Dictionary<string, object> slotData = null;
        public Dictionary<HeroKit, List<string>> custom_spells = new Dictionary<HeroKit, List<string>>();
        
        public Dictionary<string, SpellObject> original_spells = new Dictionary<string, SpellObject>();

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
                    spells.Add(SpellLookup.APSpelltoOSFESpell[$"Spell Card: {spell}"]);
                }
                custom_spells.Add(kit, spells);
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
                //TODO: hell pass
                case "Artifact Unlock":
                    UnlockArtifact(item);
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
            String osfe_spell = SpellLookup.APSpelltoOSFESpell[item.ItemDisplayName];
            UnlockSpell(osfe_spell);
            FixupSpellLists();
        }

        private void UnlockBrand(ItemInfo item)
        {
            List<String> ap_spells = SpellLookup.APBrandtoAPSpells[item.ItemDisplayName];
            foreach (String ap_spell in ap_spells)
            {
                String osfe_spell = SpellLookup.APSpelltoOSFESpell[ap_spell];
                UnlockSpell(osfe_spell);
            }
            FixupSpellLists();
        }
        
        private void UnlockArtifact(ItemInfo item)
        {
            throw new NotImplementedException();
        }

        public bool IsGoaled()
        {
            if (this.slotData == null)
            {
                return false;
            }

            int total_wins = int.Parse(slotData["total_wins"].ToString());
            int neutral_wins = int.Parse(slotData["neutral_wins"].ToString());
            int pacifist_wins = int.Parse(slotData["pacifist_wins"].ToString());
            int genocide_wins = int.Parse(slotData["genocide_wins"].ToString());

            return this.neutral_winners.Count >= neutral_wins &&
                   this.pacifist_winners.Count >= pacifist_wins &&
                   this.genocide_winners.Count >= genocide_wins &&
                   this.neutral_winners.Count + this.pacifist_winners.Count + this.genocide_winners.Count > total_wins;
        }
    }
}