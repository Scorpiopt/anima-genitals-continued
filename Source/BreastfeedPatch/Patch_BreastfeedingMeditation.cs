using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;
using UnityEngine;

namespace BreastfeedPatch
{
    [StaticConstructorOnStartup]
    internal static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var harmony = new Harmony("biasnil.breastfeedingmeditation");
            harmony.PatchAll();
            Log.Message("BreastfeedingMeditation: Harmony patches applied.");
        }
    }
}
