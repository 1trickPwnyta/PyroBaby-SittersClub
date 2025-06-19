using Verse;

namespace PyroBabySittersClub
{
    public static class Utility
    {
        public static bool IsStartingFires(this Pawn pawn)
        {
            return pawn.MentalStateDef == DefDatabase<MentalStateDef>.GetNamed("FireStartingSpree");
        }
    }
}
