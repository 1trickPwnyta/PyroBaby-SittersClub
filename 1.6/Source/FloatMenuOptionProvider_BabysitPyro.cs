using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.AI;

namespace PyroBabySittersClub
{
    public class FloatMenuOptionProvider_BabysitPyro : FloatMenuOptionProvider
    {
        protected override bool Drafted => true;

        protected override bool Undrafted => true;

        protected override bool MechanoidCanDo => true;

        protected override bool Multiselect => true;

        protected override FloatMenuOption GetSingleOptionFor(Pawn clickedPawn, FloatMenuContext context)
        {
            if (clickedPawn.IsStartingFires())
            {
                List<Pawn> ablePawns = context.ValidSelectedPawns.Where(p => !p.WorkTagIsDisabled(WorkTags.Firefighting) && p.CanReach(clickedPawn, PathEndMode.OnCell, Danger.Deadly)).ToList();
                if (ablePawns.Any())
                {
                    FloatMenuOption option = new FloatMenuOption("PyroBabySittersClub_BabysitPyro".Translate(), () =>
                    {
                        foreach (Pawn pawn in ablePawns)
                        {
                            Job job = JobMaker.MakeJob(DefDatabase<JobDef>.GetNamed("BabysitPyro"), clickedPawn);
                            job.playerForced = true;
                            pawn.jobs.TryTakeOrderedJob(job);
                        }
                    });
                    if (ablePawns.Count == 1)
                    {
                        return FloatMenuUtility.DecoratePrioritizedTask(option, ablePawns[0], clickedPawn);
                    }
                    else
                    {
                        return option;
                    }
                }
            }
            return null;
        }
    }
}
