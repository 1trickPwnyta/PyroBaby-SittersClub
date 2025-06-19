using Verse;

namespace PyroBabySittersClub
{
    public class PyroBabySittersClubMod : Mod
    {
        public const string PACKAGE_ID = "pyrobabysittersclub.1trickPwnyta";
        public const string PACKAGE_NAME = "Pyro Baby-Sitters Club";

        public PyroBabySittersClubMod(ModContentPack content) : base(content)
        {
            Log.Message($"[{PACKAGE_NAME}] Loaded.");
        }
    }
}
