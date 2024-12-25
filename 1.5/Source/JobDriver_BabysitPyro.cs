using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace PyroBabySittersClub
{
    public class JobDriver_BabysitPyro : JobDriver
    {
        private const float MAX_DISTANCE = 12f;

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return true;
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDespawnedOrNull(TargetIndex.A);

            AddEndCondition(() => TargetA.Pawn.IsStartingFires() ? JobCondition.Ongoing : JobCondition.Succeeded);

            Toil followPyro = Toils_Goto.Goto(TargetIndex.A, PathEndMode.Touch);
            followPyro.tickAction = () =>
            {
                Thing fire = GenClosest.ClosestThingReachable(pawn.Position, Map, ThingRequest.ForDef(ThingDefOf.Fire), PathEndMode.Touch, TraverseParms.For(pawn), 9999f, t =>
                {
                    return t.Position.DistanceTo(TargetA.Cell) < MAX_DISTANCE && pawn.CanReserve(t);
                });
                if (fire != null)
                {
                    job.SetTarget(TargetIndex.B, fire);
                    ReadyForNextToil();
                }
            };
            followPyro.defaultCompleteMode = ToilCompleteMode.Never;
            followPyro.JumpIf(() => !pawn.pather.Moving, followPyro);

            Toil approach = Toils_Goto.GotoThing(TargetIndex.B, PathEndMode.Touch);
            approach.JumpIfDespawnedOrNull(TargetIndex.B, followPyro);
            approach.defaultCompleteMode = ToilCompleteMode.PatherArrival;
            approach.atomicWithPrevious = true;

            Toil beat = ToilMaker.MakeToil("MakeNewToils");
            beat.tickAction = () =>
            {
                if (!pawn.CanReachImmediate(TargetB, PathEndMode.Touch))
                {
                    JumpToToil(approach);
                    return;
                }
                if (pawn.Position != TargetB.Cell && StartBeatingFireIfAnyAt(pawn.Position, beat))
                {
                    return;
                }
                pawn.natives.TryBeatFire((Fire)TargetB.Thing);
                if (TargetB.Thing.Destroyed)
                {
                    pawn.records.Increment(RecordDefOf.FiresExtinguished);
                    ReadyForNextToil();
                    return;
                }
            };
            beat.JumpIfDespawnedOrNull(TargetIndex.B, followPyro);
            beat.defaultCompleteMode = ToilCompleteMode.Never;

            yield return followPyro;
            yield return approach;
            yield return beat;
            yield return Toils_Jump.Jump(followPyro);
        }

        private bool StartBeatingFireIfAnyAt(IntVec3 cell, Toil nextToil)
        {
            foreach (Thing thing in cell.GetThingList(Map))
            {
                Fire fire = thing as Fire;
                if (fire != null && fire.parent == null)
                {
                    job.targetB = fire;
                    pawn.pather.StopDead();
                    JumpToToil(nextToil);
                    return true;
                }
            }
            return false;
        }
    }
}
