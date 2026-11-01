using ArchipelagoClient;
using HarmonyLib;
using UnityEngine;

public class FillerBuffs
{
    [HarmonyPatch(typeof(StatsScreen))]
    [HarmonyPatch("UpdateStats")]
    public class StatBuffs
    {
        static void Postfix(SpawnCtrl __instance, ref Player player)
        {
            if (player != null)
            {
                player.maxMana = player.maxMana = Mathf.Clamp(player.maxMana + (AP.I.mana_shards / 5), 0, 99);
                player.manaRegen += AP.I.bonus_mana_regen;
                player.atkDmg += AP.I.bonus_atk_dmg;
                player.spellPower += AP.I.bonus_spell_power;
                player.beingObj.defense += AP.I.bonus_defense;
                player.duelDisk.shuffleTime = Mathf.Clamp(player.duelDisk.shuffleTime - AP.I.bonus_shuffle_speed, 0.3f, float.PositiveInfinity);
            }
        }
    }

    [HarmonyPatch(typeof(PostCtrl))]
    [HarmonyPatch("CombinedLuck")]
    public class BonusLuck
    {
        static void Postfix(PostCtrl __instance, ref float __result)
        {
            __result += (float) AP.I.bonus_luck;
        }
    }
}