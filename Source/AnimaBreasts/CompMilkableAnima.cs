using HarmonyLib;
using Verse;
using RimWorld;

namespace AnimaBreasts
{
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var harmony = new Harmony("com.animabreasts.rimworld.mod");
            harmony.PatchAll();
        }
    }
}
