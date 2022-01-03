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

#if DEBUG
                    GetPointerAddresses();
#endif

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

#if DEBUG
        //Get AOB Pointer Addresses for Development of additional properties with new Offsets
        public static void GetPointerAddresses()
        {
            var ChrClassBasePtr = darkSouls.ChrClassBasePtr.Resolve();
            var ChrClassWarp = darkSouls.ChrClassWarp.Resolve();
            var WorldChrBase = darkSouls.WorldChrBase.Resolve();
            var EventFlags = darkSouls.EventFlags.Resolve();
            var ChrFollowCam = darkSouls.ChrFollowCam.Resolve();
            var ChrData1 = darkSouls.ChrData1.Resolve();
            var ChrData2 = darkSouls.ChrData2.Resolve();
            var ChrMapData = darkSouls.ChrMapData.Resolve();
            var ChrPosData = darkSouls.ChrPosData.Resolve();
            var AreaBase = darkSouls.AreaBase.Resolve();
            var CountersBase = darkSouls.CountersBase.Resolve();
        }
#endif

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
