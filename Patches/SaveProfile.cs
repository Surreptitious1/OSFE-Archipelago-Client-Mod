using ArchipelagoClient;
using HarmonyLib;
using Kittehface.Framework20;

public class SaveProfile
{
    [HarmonyPatch(typeof(RunCtrl))]
    [HarmonyPatch("_SetDefaultPrefs")]
    public class SetArchiProfile
    {
        public static void Prefix(RunCtrl __instance)
        {
        }
    }
}
