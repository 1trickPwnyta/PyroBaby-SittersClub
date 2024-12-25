using HarmonyLib;
using Verse;

namespace PyroBabySittersClub
{
    public class PyroBabySittersClubMod : Mod
    {
        public const string PACKAGE_ID = "pyrobabysittersclub.1trickPwnyta";
        public const string PACKAGE_NAME = "Pyro Baby-Sitters Club";

        public PyroBabySittersClubMod(ModContentPack content) : base(content)
        {
            var harmony = new Harmony(PACKAGE_ID);
            harmony.PatchAll();

            Log.Message($"[{PACKAGE_NAME}] Loaded.");
        }
    }
}
