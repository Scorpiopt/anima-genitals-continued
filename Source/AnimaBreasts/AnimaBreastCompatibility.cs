using HarmonyLib;
using System;
using Verse;

namespace AnimaBreasts
{
    [StaticConstructorOnStartup]
    public static class AnimaBreastCompatibility
    {
        static AnimaBreastCompatibility()
        {
            var harmony = new Harmony("biasnil.anima.breasts.compatibility");
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(PawnGenerator), "GeneratePawn", new Type[] { typeof(PawnGenerationRequest) })]
        public static class PawnGenerator_GeneratePawn_Patch
        {
            public static void Postfix(Pawn __result)
            {
                CheckAndAddLactationHediff(__result);
            }
        }

        public static void CheckAndAddLactationHediff(Pawn pawn)
        {
            if (pawn == null || pawn.health == null)
                return;

            HediffDef lactatingNaturalDef = HediffDef.Named("Lactating_Natural");
            HediffDef animaBreastsDef = HediffDef.Named("AnimaBreasts");

            // Check for existing lactation hediffs
            bool hasLactationHediff = false;
            bool hasNaturalLactationHediff = false;

            foreach (Hediff hediff in pawn.health.hediffSet.hediffs)
            {
                if (hediff.def.defName.Contains("Lactating"))
                {
                    hasLactationHediff = true;
                    if (hediff.def == lactatingNaturalDef)
                    {
                        hasNaturalLactationHediff = true;
                    }
                }
            }

            if (hasLactationHediff && !hasNaturalLactationHediff && pawn.health.hediffSet.HasHediff(animaBreastsDef))
            {
                Hediff newHediff = HediffMaker.MakeHediff(lactatingNaturalDef, pawn);
                pawn.health.AddHediff(newHediff);
            }
        }
    }
}
