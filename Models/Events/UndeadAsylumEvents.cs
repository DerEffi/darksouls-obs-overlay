namespace DarkSoulsOBSOverlay.Models.Events
{
    public static class UndeadAsylumEvents
    {
        public static int AsylumDeamon { get; } = 16; //defeated
        public static int PreOscarFog { get; } = 11810090; //cleared
        public static int CellDoor { get; } = 11810103; //opened
        public static int F2WestDoor { get; } = 11810104; //opened
        public static int ShortcutDoor { get; } = 11810105; //opened
        public static int F2EastDoor { get; } = 11810106; //opened
        public static int BigPilgrimDoor { get; } = 11810110; //opened
        public static int OscarTrap { get; } = 11810211; //sprung
        public static int OscarGiftEstus { get; set; } = 11810590; //obtained
        public static int OscarGiftF2EastKey { get; set; } = 11810591; //obtained
        public static int BossDoor { get; set; } = 11810112; //opened
    }
}
