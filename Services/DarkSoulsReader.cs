using DarkSoulsOBSOverlay.Models;
using DarkSoulsOBSOverlay.Models.Events;
using DarkSoulsOBSOverlay.Services.AOB;
using Newtonsoft.Json;

namespace DarkSoulsOBSOverlay.Services
{
    public static class DarkSoulsReader
    {
        private static DSRHook darkSouls = null;
        private static DarkSoulsData lastStats = null;
        public static Settings settings = new();

        public static DarkSoulsData GetCurrentStats()
        {
            if(darkSouls == null)
            {
                darkSouls = new DSRHook(5000, 5000);
                darkSouls.Start();
            } else if(darkSouls.Hooked)
            {
                try
                {
                    //For dev only
                    //GetPointerAddresses();

                    return new DarkSoulsData
                    {
                        Settings = settings,
                        Connected = true,
                        Loaded = darkSouls.Loaded,
                        Version = darkSouls.Version,
                        CharacterName = darkSouls.CharName,
                        CharacterClass = darkSouls.GetClass(),
                        CharacterClassId = darkSouls.Class,
                        Health = settings.HealthEnabled ? darkSouls.Health : 0,
                        HealthMax = settings.HealthEnabled ? darkSouls.HealthMax : 0,
                        LastBonfire = darkSouls.GetLastBonfire(),
                        LastBonfireId = darkSouls.LastBonfire,
                        Area = darkSouls.GetArea(),
                        AreaId = darkSouls.Area,
                        Deaths = darkSouls.Deaths,
                        Clock = settings.ClockEnabled ? darkSouls.GetSeconds() : 0,
                        UndeadAsylum = new()
                        {
                            PreOscarFog = darkSouls.ReadEventFlag(UndeadAsylumEvents.PreOscarFog),
                            CellDoor = darkSouls.ReadEventFlag(UndeadAsylumEvents.CellDoor),
                            F2WestDoor = darkSouls.ReadEventFlag(UndeadAsylumEvents.F2WestDoor),
                            ShortcutDoor = darkSouls.ReadEventFlag(UndeadAsylumEvents.ShortcutDoor),
                            F2EastDoor = darkSouls.ReadEventFlag(UndeadAsylumEvents.F2EastDoor),
                            BigPilgrimDoor = darkSouls.ReadEventFlag(UndeadAsylumEvents.BigPilgrimDoor),
                            OscarTrapSprung = darkSouls.ReadEventFlag(UndeadAsylumEvents.OscarTrapSprung),
                            AsylumDeamon = darkSouls.ReadEventFlag(UndeadAsylumEvents.AsylumDeamon),
                        },
                        FirelinkShrine = new()
                        {
                            TalismanChest = darkSouls.ReadEventFlag(FirelinkShrineEvents.TalismanChest),
                            HomewardBoneChest = darkSouls.ReadEventFlag(FirelinkShrineEvents.HomewardBoneChest),
                            CrackedRedEyeOrbChest = darkSouls.ReadEventFlag(FirelinkShrineEvents.CrackedRedEyeOrbChest),
                            LloydTalismanChest = darkSouls.ReadEventFlag(FirelinkShrineEvents.LloydTalismanChest)
                        }
                    };
                } catch
                {
                    return new();
                }
            }

            return new();
        }

        //Get AOB Pointer Addresses for Development of additional properties with new Offsets
        public static void GetPointerAddresses()
        {
            var test1 = darkSouls.ChrClassBasePtr.Resolve();
            var test2 = darkSouls.ChrClassWarp.Resolve();
            var test3 = darkSouls.WorldChrBase.Resolve();
            var test4 = darkSouls.EventFlags.Resolve();
            var test5 = darkSouls.ChrFollowCam.Resolve();
            var test6 = darkSouls.ChrData1.Resolve();
            var test7 = darkSouls.ChrData2.Resolve();
            var test8 = darkSouls.ChrMapData.Resolve();
            var test9 = darkSouls.ChrPosData.Resolve();
            var test10 = darkSouls.AreaBase.Resolve();
            var test11 = darkSouls.CountersBase.Resolve();
        }

        public static void SendDarkSoulsData()
        {
            DarkSoulsData stats = GetCurrentStats();
            if (stats != null && !Helper.AreEqual(lastStats, stats))
            {
                try
                {
                    CommunicationService.SendMessage(JsonConvert.SerializeObject(stats), false);
                    lastStats = stats;
                }
                catch { }
            }
        }

        public static void ClearStats()
        {
            lastStats = null;
        }
    }
}
