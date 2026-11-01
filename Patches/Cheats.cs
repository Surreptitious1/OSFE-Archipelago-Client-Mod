using System.Runtime.CompilerServices;
using HarmonyLib;

namespace ArchipelagoClient.Patches
{
    public class Cheats
    {
        // Cheats for testing, do not ship
        /*
        [HarmonyPatch(typeof(SpawnCtrl))]
        [HarmonyPatch("CreateEnemy")]
        class OneHealthEnemySpawn
        {
            static void Postfix(SpawnCtrl __instance, ref Cpu newCpu)
            {
                Enemy component = newCpu.GetComponent<Enemy>();
                component.health.SetHealth(1);
            }
        }

        [HarmonyPatch(typeof(SpawnCtrl))]
        [HarmonyPatch("SpawnPlayer")]
        class GodModePlayer
        {
            static void Postfix(SpawnCtrl __instance, ref Player __result)
            {
                __result.health.SetHealth(65535, 65535);
            }
        }

        [HarmonyPatch(typeof(StatsScreen))]
        [HarmonyPatch("UpdateStats")]
        class PlayerDamageUp
        {
            static void Postfix(SpawnCtrl __instance, ref Player player)
            {
                if (player != null)
                {
                    player.atkDmg = 999;
                }
            }
        }

        // Couldn't get this one working
        //[HarmonyPatch(typeof(XMLReader))]
        //[HarmonyPatch("XMLtoGetWorlds")]
        //public class ShortWorlds
        //{
        //}
        */
    }
}