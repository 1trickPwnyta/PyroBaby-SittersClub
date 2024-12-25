namespace PyroBaby-SittersClub
{
    public static class Debug
    {
        public static void Log(string message)
        {
#if DEBUG
            Verse.Log.Message($"[{PyroBaby-SittersClubMod.PACKAGE_NAME}] {message}");
#endif
        }
    }
}
