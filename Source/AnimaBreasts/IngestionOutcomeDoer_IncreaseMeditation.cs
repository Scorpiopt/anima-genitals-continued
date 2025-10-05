using Verse;
using RimWorld;
using System.Collections.Generic;

namespace AnimaBreasts
{
    public class IngestionOutcomeDoer_IncreaseMeditation : IngestionOutcomeDoer
    {
        public float meditationIncrease = 0.1f;

        protected override void DoIngestionOutcomeSpecial(Pawn pawn, Thing ingested, int ingestedCount)
        {
            if (pawn == null)
                return;

            // Apply meditation focus gain
            if (pawn.psychicEntropy != null)
            {
                pawn.psychicEntropy.OffsetPsyfocusDirectly(meditationIncrease);
                Messages.Message("PawnGainedMeditationFocus".Translate(pawn.LabelShort, meditationIncrease.ToStringPercent()), pawn, MessageTypeDefOf.PositiveEvent);
            }
        }

        public override IEnumerable<StatDrawEntry> SpecialDisplayStats(ThingDef parentDef)
        {
            yield return new StatDrawEntry(StatCategoryDefOf.Basics, "Meditation Increase", meditationIncrease.ToStringPercent(), "This increases the pawn's meditation focus by this percentage.", 0);
        }
    }
}
