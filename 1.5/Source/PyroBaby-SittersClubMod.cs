using HarmonyLib;
using Verse;

namespace PyroBaby-SittersClub
{
    public class PyroBaby-SittersClubMod : Mod
    {
        public const string PACKAGE_ID = "pyrobaby-sittersclub.1trickPwnyta";
        public const string PACKAGE_NAME = "Pyro Baby-Sitters Club";

        public PyroBaby-SittersClubMod(ModContentPack content) : base(content)
        {
            var harmony = new Harmony(PACKAGE_ID);
            harmony.PatchAll();

            Log.Message($"[{PACKAGE_NAME}] Loaded.");
        }
    }
}
