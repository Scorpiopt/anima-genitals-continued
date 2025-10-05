using HarmonyLib;
using RimWorld;
using rjw;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace AnimaGenitals
{
    [StaticConstructorOnStartup]
    public static class AnimaGenitalsPsyfocusBoost
    {
        static AnimaGenitalsPsyfocusBoost()
        {
            var harmony = new Harmony("Bunuffin.Biasnil.animagenitals");
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(SexUtility), "Aftersex")]
        public static class Patch_Aftersex
        {
            [HarmonyPostfix]
            public static void Postfix(SexProps props)
            {
                Pawn pawn = props.pawn;
                Pawn partner = props.partner;

                bool pawnHasAnimaGenitals = HasAnimaGenitals(pawn);
                bool partnerHasAnimaGenitals = HasAnimaGenitals(partner);

                if (pawnHasAnimaGenitals || partnerHasAnimaGenitals)
                {
                    if (pawnHasAnimaGenitals && partnerHasAnimaGenitals)
                    {
                        AddOrIncreaseBond(pawn, 0.1f);
                        AddOrIncreaseBond(partner, 0.1f);
                    }
                    else
                    {
                        AddOrIncreaseBond(pawnHasAnimaGenitals ? pawn : partner, 0.1f);
                    }
                }
            }

            private static bool HasAnimaGenitals(Pawn pawn)
            {
                return pawn.health.hediffSet.HasHediff(HediffDef.Named("AnimaPenis")) ||
                       pawn.health.hediffSet.HasHediff(HediffDef.Named("AnimaVagina")) ||
                       pawn.health.hediffSet.HasHediff(HediffDef.Named("AnimaAnus"));
            }

            private static void AddOrIncreaseBond(Pawn pawn, float severityIncrease)
            {
                if (pawn != null)
                {
                    Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDef.Named("AnimaTicPsychicBond"));
                    if (hediff != null)
                    {
                        hediff.Severity += severityIncrease;
                    }
                    else
                    {
                        hediff = HediffMaker.MakeHediff(HediffDef.Named("AnimaTicPsychicBond"), pawn);
                        hediff.Severity = severityIncrease;
                        pawn.health.AddHediff(hediff);
                    }
                }
            }
        }
        [StaticConstructorOnStartup]
        public static class AnimaGenitalsBirthPatch
        {
            static AnimaGenitalsBirthPatch()
            {
                var harmony = new Harmony("com.yourname.animagenitals.birthpatch");
                harmony.PatchAll();
            }

            [HarmonyPatch(typeof(PregnancyUtility))]
            [HarmonyPatch("ApplyBirthOutcome")]
            public static class Patch_ApplyBirthOutcome_NewTemp
            {
                [HarmonyPostfix]
                public static void ApplyBirthOutcomePostfix(RitualOutcomePossibility outcome, float quality, Precept_Ritual ritual, List<GeneDef> genes, Pawn geneticMother, Thing birtherThing, Pawn father = null, Pawn doctor = null, LordJob_Ritual lordJobRitual = null, RitualRoleAssignments assignments = null, bool preventLetter = false)
                {
                    if (birtherThing is Pawn mother && HasAnimaVagina(mother))
                    {
                        Pawn lastBorn = geneticMother.relations.Children.LastOrDefault();
                        if (lastBorn != null)
                        {
                            AddAnimabornHediff(lastBorn);
                            ApplyStartingHediffs(lastBorn);
                        }
                    }
                }

                private static bool HasAnimaVagina(Pawn pawn)
                {
                    return pawn.health?.hediffSet?.HasHediff(HediffDef.Named("AnimaVagina")) == true;
                }

                private static void AddAnimabornHediff(Pawn pawn)
                {
                    Hediff animaborn = HediffMaker.MakeHediff(HediffDef.Named("Animaborn"), pawn);
                    pawn.health.AddHediff(animaborn);
                }

                private static void ApplyStartingHediffs(Pawn pawn)
                {
                    if (!pawn.kindDef.startingHediffs.NullOrEmpty())
                    {
                        HealthUtility.AddStartingHediffs(pawn, pawn.kindDef.startingHediffs);
                    }
                }
            }
        }
    }

}
