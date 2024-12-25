using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace PyroBabySittersClub
{
    [HarmonyPatch(typeof(Pawn))]
    [HarmonyPatch(nameof(Pawn.GetFloatMenuOptions))]
    public static class Patch_Pawn
    {
        public static void Postfix(Pawn __instance, Pawn selPawn, ref IEnumerable<FloatMenuOption> __result)
        {
            if (__instance.IsStartingFires() && !selPawn.WorkTagIsDisabled(WorkTags.Firefighting) && selPawn.CanReach(__instance, Verse.AI.PathEndMode.OnCell, Danger.Deadly))
            {
                FloatMenuOption option = FloatMenuUtility.DecoratePrioritizedTask(new FloatMenuOption("PyroBabySittersClub_BabysitPyro".Translate(), delegate ()
                {
                    Job job = JobMaker.MakeJob(DefDatabase<JobDef>.GetNamed("BabysitPyro"), __instance);
                    job.playerForced = true;
                    selPawn.jobs.TryTakeOrderedJob(job);
                }), selPawn, __instance);
                __result = __result.AddItem(option);
            }
        }
    }
}
