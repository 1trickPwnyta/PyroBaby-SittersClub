namespace PyroBabySittersClub
{
    public static class Debug
    {
        public static void Log(object message)
        {
#if DEBUG
            Verse.Log.Message($"[{PyroBabySittersClubMod.PACKAGE_NAME}] {message}");
#endif
        }
    }
}
