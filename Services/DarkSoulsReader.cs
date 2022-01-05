using DarkSoulsOBSOverlay.Models;
using DarkSoulsOBSOverlay.Models.Events;
using DarkSoulsOBSOverlay.Services.AOB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;

namespace DarkSoulsOBSOverlay.Services
{
    public static class DarkSoulsReader
    {
        private static DSRHook darkSouls = null;
        private static DarkSoulsData SavedStats = new();
        public static Settings Settings = new();
        public static Timer Timer = new()
        {
            Interval = Settings.UpdateInterval * 1000,
            AutoReset = true,
        };

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
                    //GetPointerAddresses();
#endif

                    DarkSoulsData Stats = Helper.Clone(SavedStats);

                    //Base Data that always gets reloaded
                    Stats.Settings = Settings;
                    Stats.Connected = true;
                    Stats.Loaded = darkSouls.Loaded;
                    Stats.Version = darkSouls.Version;
                    Stats.Clock = darkSouls.GetSeconds();
                    Stats.UpdatedEventFlags = Settings.CompareEventFlags ? darkSouls.CompareEventFlags() : new();
                    Stats.Char = new()
                    {
                        CharacterName = darkSouls.CharName,
                        CharacterClass = darkSouls.GetClass(),
                        CharacterClassId = darkSouls.Class,
                        Health = darkSouls.Health,
                        HealthMax = darkSouls.HealthMax,
                        LastBonfire = darkSouls.GetLastBonfire(),
                        LastBonfireId = darkSouls.LastBonfire,
                        Area = darkSouls.GetArea(),
                        AreaId = darkSouls.Area,
                        Deaths = darkSouls.Deaths,
                    };
                    Stats.Common = new() {
                        WeaponSmithbox = darkSouls.ReadEventFlag(CommonEvents.WeaponSmithbox),
                        ArmorSmithbox = darkSouls.ReadEventFlag(CommonEvents.ArmorSmithbox),
                        Repairbox = darkSouls.ReadEventFlag(CommonEvents.Repairbox),
                        BottomlessBox = darkSouls.ReadEventFlag(CommonEvents.BottomlessBox),
                        EstusFlask = darkSouls.ReadEventFlag(CommonEvents.EstusFlask),
                        FirstSpell = darkSouls.ReadEventFlag(CommonEvents.FirstSpell),
                        AbleToPayAbsolution = darkSouls.ReadEventFlag(CommonEvents.AbleToPayAbsolution),
                        TitaniteShard = darkSouls.ReadEventFlag(CommonEvents.TitaniteShard),
                        LargeTitaniteShard = darkSouls.ReadEventFlag(CommonEvents.LargeTitaniteShard),
                        GreenTitaniteShard = darkSouls.ReadEventFlag(CommonEvents.GreenTitaniteShard),
                        TitaniteChunk = darkSouls.ReadEventFlag(CommonEvents.TitaniteChunk),
                        BlueTitaniteChunk = darkSouls.ReadEventFlag(CommonEvents.BlueTitaniteChunk),
                        WhiteTitaniteChunk = darkSouls.ReadEventFlag(CommonEvents.WhiteTitaniteChunk),
                        RedTitaniteChunk = darkSouls.ReadEventFlag(CommonEvents.RedTitaniteChunk),
                        TitaniteSlab = darkSouls.ReadEventFlag(CommonEvents.TitaniteSlab),
                        BlueTitaniteSlab = darkSouls.ReadEventFlag(CommonEvents.BlueTitaniteSlab),
                        WhiteTitaniteSlab = darkSouls.ReadEventFlag(CommonEvents.WhiteTitaniteSlab),
                        RedTitaniteSlab = darkSouls.ReadEventFlag(CommonEvents.RedTitaniteSlab),
                        DragonScale = darkSouls.ReadEventFlag(CommonEvents.DragonScale),
                        DemonTitanite = darkSouls.ReadEventFlag(CommonEvents.DemonTitanite),
                        TwinklingTitanite = darkSouls.ReadEventFlag(CommonEvents.TwinklingTitanite),

                        NoCovenant = darkSouls.ReadEventFlag(CommonEvents.NoCovenant),
                        WayOfWhite = darkSouls.ReadEventFlag(CommonEvents.WayOfWhite),
                        WarriorOfSunlight = darkSouls.ReadEventFlag(CommonEvents.WarriorOfSunlight),
                        Darkwraith = darkSouls.ReadEventFlag(CommonEvents.Darkwraith),
                        PathOfTheDragon = darkSouls.ReadEventFlag(CommonEvents.PathOfTheDragon),
                        GravelordServant = darkSouls.ReadEventFlag(CommonEvents.GravelordServant),
                        ForestHunter = darkSouls.ReadEventFlag(CommonEvents.ForestHunter),
                        Darkmoon = darkSouls.ReadEventFlag(CommonEvents.Darkmoon),
                        ChaosServant = darkSouls.ReadEventFlag(CommonEvents.ChaosServant),
                    };

                    if(SavedStats.Char.LastBonfireId == -1 && Stats.Char.LastBonfireId != -1)
                    {
                        //Reload everything on new SaveGame Load
                        Stats.AnorLondo = ReadAnorLondoData();
                        Stats.Blighttown = ReadBlighttownData();
                        Stats.Catacombs = ReadCatacombsData();
                        Stats.ChasmOfTheAbyss = ReadChasmOfTheAbyssData();
                        Stats.CrystalCave = ReadCrystalCaveData();
                        Stats.DarkrootGarden = ReadDarkrootGardenData();
                        Stats.DemonRuins = ReadDemonRuinsData();
                        Stats.Depths = ReadDepthsData();
                        Stats.FirelinkShrine = ReadFirelinkShrineData();
                        Stats.KilnOfTheFirstFlame = ReadKilnOfTheFirstFlameData();
                        Stats.LostIzalith = ReadLostIzalithData();
                        Stats.NewLondoRuins = ReadNewLondoRuinsData();
                        Stats.OolacileTownship = ReadOolacileTownshipData();
                        Stats.PaintedWorld = ReadPaintedWorldData();
                        Stats.QueelagsDomain = ReadQueelagsDomainData();
                        Stats.RoyalWoods = ReadRoyalWoodsData();
                        Stats.SanctuaryGarden = ReadSanctuaryGardenData();
                        Stats.SensFortress = ReadSensFortressData();
                        Stats.TombOfGiants = ReadTombOfGiantsData();
                        Stats.UndeadAsylum = ReadUndeadAsylumData();
                        Stats.UndeadBurg = ReadUndeadBurgData();
                        Stats.UndeadParish = ReadUndeadParishData();
                        Stats.ValleyOfDrakes = ReadValleyOfDrakesData();
                    } else {
                        //Reload only the Area where the char is
                        switch(Stats.Char.AreaId) {
                            case 1510:
                            case 2002:
                            case 2003: //Chamber of the Princess
                            case 2004: //Darkmoon Tomb
                                Stats.AnorLondo = ReadAnorLondoData();
                                break;
                            case 1400:
                                Stats.Blighttown = ReadBlighttownData();
                                break;
                            case 1300:
                            case 2020:
                                Stats.Catacombs = ReadCatacombsData();
                                break;
                            case 1214:
                            case 2012:
                                Stats.ChasmOfTheAbyss = ReadChasmOfTheAbyssData();
                                break;
                            case 1701:
                            case 2019:
                                Stats.CrystalCave = ReadCrystalCaveData();
                                break;
                            case 1200:
                                Stats.DarkrootGarden = ReadDarkrootGardenData();
                                break;
                            case 1410:
                                Stats.DemonRuins = ReadDemonRuinsData();
                                break;
                            case 1000:
                            case 2014:
                                Stats.Depths = ReadDepthsData();
                                break;
                            case 1020:
                            case 2000:
                                Stats.FirelinkShrine = ReadFirelinkShrineData();
                                break;
                            case 1800:
                            case 1801:
                                Stats.KilnOfTheFirstFlame = ReadKilnOfTheFirstFlameData();
                                break;
                            case 1411:
                                Stats.LostIzalith = ReadLostIzalithData();
                                break;
                            case 1600:
                            case 1610: //Abyss
                            case 2005: //Abyss
                                Stats.NewLondoRuins = ReadNewLondoRuinsData();
                                break;
                            case 1213:
                            case 2011:
                            case 2013: //Dungeon
                                Stats.OolacileTownship = ReadOolacileTownshipData();
                                break;
                            case 1100:
                            case 2016:
                                Stats.PaintedWorld = ReadPaintedWorldData();
                                break;
                            case 1401:
                            case 2006: //Daughter of Chaos
                                Stats.QueelagsDomain = ReadQueelagsDomainData();
                                break;
                            case 1212:
                                Stats.RoyalWoods = ReadRoyalWoodsData();
                                break;
                            case 1210:
                            case 2009:
                                Stats.SanctuaryGarden = ReadSanctuaryGardenData();
                                break;
                            case 1500:
                                Stats.SensFortress = ReadSensFortressData();
                                break;
                            case 1310:
                            case 2007: //Gravelord Altar
                            case 2017:
                                Stats.TombOfGiants = ReadTombOfGiantsData();
                                break;
                            case 1810:
                                Stats.UndeadAsylum = ReadUndeadAsylumData();
                                break;
                            case 1010:
                                Stats.UndeadBurg = ReadUndeadBurgData();
                                break;
                            case 1011:
                            case 2001: //Sunlight Altar
                            case 2015:
                                Stats.UndeadParish = ReadUndeadParishData();
                                break;
                            case 1602:
                                Stats.ValleyOfDrakes = ReadValleyOfDrakesData();
                                break;
                        }
                    }

                    return Stats;
                    
                } catch (Exception e) {
#if DEBUG
                    Debug.Write(e.ToString());
#endif
                }
            }

            return new();
        }

        public static void SendDarkSoulsData(bool force = false)
        {
            DarkSoulsData LastStats = Helper.Clone(SavedStats);
            SavedStats = GetCurrentStats();
            if (force || !Helper.StatsAreEqual(LastStats, SavedStats))
            {
                try
                {
                    CommunicationService.SendMessage(JsonConvert.SerializeObject(SavedStats), false);
#if DEBUG
                    Debug.WriteLine("Update detected: Sending data through websocket");
#endif
                }
                catch {}
            }
        }

#region ReadEventFlags

        public static AnorLondoData ReadAnorLondoData()
        {
            return new() {
                MainHallDoor = darkSouls.ReadEventFlag(AnorLondoEvents.MainHallDoor),
                BlacksmithShortcutDoor = darkSouls.ReadEventFlag(AnorLondoEvents.BlacksmithShortcutDoor),
                SolaireBonfireShortcutDoor = darkSouls.ReadEventFlag(AnorLondoEvents.SolaireBonfireShortcutDoor),
                Dark = darkSouls.ReadEventFlag(AnorLondoEvents.Dark),
                Gwyndolin = darkSouls.ReadEventFlag(AnorLondoEvents.Gwyndolin),
                OrnsteinSmough = darkSouls.ReadEventFlag(AnorLondoEvents.OrnsteinSmough),
            };
        }

        public static BlighttownData ReadBlighttownData()
        {
            return new() {
                ChaosWitchQueelag = darkSouls.ReadEventFlag(BlighttownEvents.ChaosWitchQueelag),
            };
        }

        public static CatacombsData ReadCatacombsData()
        {
            return new() {
                Pinwheel = darkSouls.ReadEventFlag(CatacombsEvents.Pinwheel),
            };
        }

        public static ChasmOfTheAbyssData ReadChasmOfTheAbyssData()
        {
            return new() {
                Manus = darkSouls.ReadEventFlag(ChasmOfTheAbyssEvents.Manus),
            };
        }

        public static CrystalCaveData ReadCrystalCaveData()
        {
            return new() {
                Seath = darkSouls.ReadEventFlag(CrystalCaveEvents.Seath),
            };
        }

        public static DarkrootGardenData ReadDarkrootGardenData()
        {
            return new() {
                MoonlightButterfly = darkSouls.ReadEventFlag(DarkrootGardenEvents.MoonlightButterfly),
                CatCovenantRing = darkSouls.ReadEventFlag(DarkrootGardenEvents.CatCovenantRing),
                Sif = darkSouls.ReadEventFlag(DarkrootGardenEvents.Sif),
            };
        }

        public static DemonRuinsData ReadDemonRuinsData()
        {
            return new() {
                QueelagsDomainElevator = darkSouls.ReadEventFlag(DemonRuinsEvents.QueelagsDomainElevator),
                DemonFiresage = darkSouls.ReadEventFlag(DemonRuinsEvents.DemonFiresage),
                CeaselessDischarge = darkSouls.ReadEventFlag(DemonRuinsEvents.CeaselessDischarge),
                CentipedeDemon = darkSouls.ReadEventFlag(DemonRuinsEvents.CentipedeDemon),
            };
        }

        public static DepthsData ReadDepthsData()
        {
            return new() {
                BlighttownDoor = darkSouls.ReadEventFlag(DepthsEvents.BlighttownDoor),
                GapingDragon = darkSouls.ReadEventFlag(DepthsEvents.GapingDragon),
            };
        }

        public static FirelinkShrineData ReadFirelinkShrineData()
        {
            return new() {
                TalismanChest = darkSouls.ReadEventFlag(FirelinkShrineEvents.TalismanChest),
                HomewardBoneChest = darkSouls.ReadEventFlag(FirelinkShrineEvents.HomewardBoneChest),
                CrackedRedEyeOrbChest = darkSouls.ReadEventFlag(FirelinkShrineEvents.CrackedRedEyeOrbChest),
                LloydTalismanChest = darkSouls.ReadEventFlag(FirelinkShrineEvents.LloydTalismanChest),
                CurledUpLikeABall = darkSouls.ReadEventFlag(FirelinkShrineEvents.CurledUpLikeABall),
                PyromancyFlame = darkSouls.ReadEventFlag(FirelinkShrineEvents.PyromancyFlame),
                PetrusCopperCoin = darkSouls.ReadEventFlag(FirelinkShrineEvents.PetrusCopperCoin),
            };
        }

        public static KilnOfTheFirstFlameData ReadKilnOfTheFirstFlameData()
        {
            return new() {
                Gwyn = darkSouls.ReadEventFlag(KilnOfTheFirstFlameEvents.Gwyn),
            };
        }

        public static LostIzalithData ReadLostIzalithData()
        {
            return new() {
                BedOfChaos = darkSouls.ReadEventFlag(LostIzalithEvents.BedOfChaos),
            };
        }

        public static NewLondoRuinsData ReadNewLondoRuinsData()
        {
            return new() {
                Drained = darkSouls.ReadEventFlag(NewLondoRuinsEvents.Drained),
                DoorToTheSeal = darkSouls.ReadEventFlag(NewLondoRuinsEvents.DoorToTheSeal),
                ValleyOfDrakesDoor = darkSouls.ReadEventFlag(NewLondoRuinsEvents.ValleyOfDrakesDoor),
                ShortcutLadder = darkSouls.ReadEventFlag(NewLondoRuinsEvents.ShortcutLadder),
                FirelinkElevator = darkSouls.ReadEventFlag(NewLondoRuinsEvents.FirelinkElevator),
                EstocCorpse = darkSouls.ReadEventFlag(NewLondoRuinsEvents.EstocCorpse),
                FourKings = darkSouls.ReadEventFlag(NewLondoRuinsEvents.FourKings),
            };
        }

        public static OolacileTownshipData ReadOolacileTownshipData()
        {
            return new() {
                CrestKeyDoor = darkSouls.ReadEventFlag(OolacileTownshipEvents.CrestKeyDoor),
            };
        }

        public static PaintedWorldData ReadPaintedWorldData()
        {
            return new() {
                Priscilla = darkSouls.ReadEventFlag(PaintedWorldEvents.Priscilla),
            };
        }

        public static QueelagsDomainData ReadQueelagsDomainData()
        {
            return new() {
                BellOfAwakening = darkSouls.ReadEventFlag(QueelagsDomainEvents.BellOfAwakening),
            };
        }

        public static RoyalWoodsData ReadRoyalWoodsData()
        {
            return new() {
                Kalameet = darkSouls.ReadEventFlag(RoyalWoodsEvents.Kalameet),
                Artorias = darkSouls.ReadEventFlag(RoyalWoodsEvents.Artorias),
            };
        }

        public static SanctuaryGardenData ReadSanctuaryGardenData()
        {
            return new() {
                SanctuaryGuardian = darkSouls.ReadEventFlag(SanctuaryGardenEvents.SanctuaryGuardian),
            };
        }

        public static SensFortressData ReadSensFortressData()
        {
            return new() {
                CageElevator = darkSouls.ReadEventFlag(SensFortressEvents.CageElevator),
                MainGate = darkSouls.ReadEventFlag(SensFortressEvents.MainGate),
                IronGolem = darkSouls.ReadEventFlag(SensFortressEvents.IronGolem),
            };
        }

        public static TombOfGiantsData ReadTombOfGiantsData()
        {
            return new() {
                Nito = darkSouls.ReadEventFlag(TombOfGiantsEvents.Nito),
            };
        }

        public static UndeadAsylumData ReadUndeadAsylumData()
        {
            return new() {
                AsylumDeamon = darkSouls.ReadEventFlag(UndeadAsylumEvents.AsylumDeamon),
                PreOscarFog = darkSouls.ReadEventFlag(UndeadAsylumEvents.PreOscarFog),
                CellDoor = darkSouls.ReadEventFlag(UndeadAsylumEvents.CellDoor),
                F2WestDoor = darkSouls.ReadEventFlag(UndeadAsylumEvents.F2WestDoor),
                ShortcutDoor = darkSouls.ReadEventFlag(UndeadAsylumEvents.ShortcutDoor),
                F2EastDoor = darkSouls.ReadEventFlag(UndeadAsylumEvents.F2EastDoor),
                BigPilgrimDoor = darkSouls.ReadEventFlag(UndeadAsylumEvents.BigPilgrimDoor),
                OscarTrap = darkSouls.ReadEventFlag(UndeadAsylumEvents.OscarTrap),
                OscarGiftEstus = darkSouls.ReadEventFlag(UndeadAsylumEvents.OscarGiftEstus),
                OscarGiftF2EastKey = darkSouls.ReadEventFlag(UndeadAsylumEvents.OscarGiftF2EastKey),
                BossDoor = darkSouls.ReadEventFlag(UndeadAsylumEvents.BossDoor),
            };
        }

        public static UndeadBurgData ReadUndeadBurgData()
        {
            return new() {
                TaurusDeamon = darkSouls.ReadEventFlag(UndeadBurgEvents.TaurusDeamon),
                CapraDeamon = darkSouls.ReadEventFlag(UndeadBurgEvents.CapraDeamon),
                BlackKnight = darkSouls.ReadEventFlag(UndeadBurgEvents.BlackKnight),
                HellkiteFirst = darkSouls.ReadEventFlag(UndeadBurgEvents.HellkiteFirst),
                HellkiteSecond = darkSouls.ReadEventFlag(UndeadBurgEvents.HellkiteSecond),
                FogGate = darkSouls.ReadEventFlag(UndeadBurgEvents.FogGate),
                ShortcutLadder = darkSouls.ReadEventFlag(UndeadBurgEvents.ShortcutLadder),
                FlamingBarrel = darkSouls.ReadEventFlag(UndeadBurgEvents.FlamingBarrel),
                LowerBurgShortcut = darkSouls.ReadEventFlag(UndeadBurgEvents.LowerBurgShortcut),
                BasementKeyDoor = darkSouls.ReadEventFlag(UndeadBurgEvents.BasementKeyDoor),
                WatchtowerUpperDoor = darkSouls.ReadEventFlag(UndeadBurgEvents.WatchtowerUpperDoor),
                WatchtowerLowerDoor = darkSouls.ReadEventFlag(UndeadBurgEvents.WatchtowerLowerDoor),
                SublightALtasGate = darkSouls.ReadEventFlag(UndeadBurgEvents.SublightALtasGate),
                BlackFirebombChest = darkSouls.ReadEventFlag(UndeadBurgEvents.BlackFirebombChest),
                GoldPineResinChest = darkSouls.ReadEventFlag(UndeadBurgEvents.GoldPineResinChest),
            };
        }

        public static UndeadParishData ReadUndeadParishData()
        {
            return new() {
                FirelinkElevator = darkSouls.ReadEventFlag(UndeadParishEvents.FirelinkElevator),
                AltarOfSunlight = darkSouls.ReadEventFlag(UndeadParishEvents.AltarOfSunlight),
                BerenikeKnight = darkSouls.ReadEventFlag(UndeadParishEvents.BerenikeKnight),
                Channeler = darkSouls.ReadEventFlag(UndeadParishEvents.Channeler),
                BellOfAwakening = darkSouls.ReadEventFlag(UndeadParishEvents.BellOfAwakening),
                LightningSpear = darkSouls.ReadEventFlag(UndeadParishEvents.LightningSpear),
                BellGargoyles = darkSouls.ReadEventFlag(UndeadParishEvents.BellGargoyles),
            };
        }

        public static ValleyOfDrakesData ReadValleyOfDrakesData()
        {
            return new() {
                DarkrootElevator = darkSouls.ReadEventFlag(ValleyOfDrakesEvents.DarkrootElevator),
                DarkrootElevatorInoperable = darkSouls.ReadEventFlag(ValleyOfDrakesEvents.DarkrootElevatorInoperable),
                KeyToTheSeal = darkSouls.ReadEventFlag(ValleyOfDrakesEvents.KeyToTheSeal),
            };
        }

#endregion

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

    }
}
