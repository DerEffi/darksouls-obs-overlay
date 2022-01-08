using DarkSoulsOBSOverlay.Models;
using DarkSoulsOBSOverlay.Models.Events;
using DarkSoulsOBSOverlay.Models.Mappings;
using DarkSoulsOBSOverlay.Services.AOB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Timers;

namespace DarkSoulsOBSOverlay.Services
{
    public static class DarkSoulsReader
    {
        private static DSRHook darkSouls = null;
        private static DarkSoulsData SavedStats = new();
        private static DarkSoulsResettedData ResettedStats = new();
        private static Settings _settings = new();
        public readonly static Timer Timer = new()
        {
            Interval = 1000,
            AutoReset = true,
        };

        public static Settings GetSettings()
        {
            return _settings;
        }

        public static void SetSettings(Settings settings)
        {
            _settings = settings;
            Timer.Interval = settings.UpdateInterval * 1000;
            FileService.SaveSettings(settings);
        }

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
                    Stats.Settings = _settings;
                    Stats.Connected = true;
                    Stats.Loaded = darkSouls.Loaded;
                    Stats.Version = darkSouls.Version;
                    Stats.UpdatedEventFlags = _settings.CompareEventFlags ? darkSouls.CompareEventFlags() : new();
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
                        Clock = darkSouls.GetSeconds(),
                        SaveSlot = darkSouls.SaveSlot,
                    };
                    Stats.Events.Common = new()
                    {
                        WeaponSmithbox = darkSouls.ReadEventFlag(Mappings.EventFlags.Common.WeaponSmithbox),
                        ArmorSmithbox = darkSouls.ReadEventFlag(Mappings.EventFlags.Common.ArmorSmithbox),
                        Repairbox = darkSouls.ReadEventFlag(Mappings.EventFlags.Common.Repairbox),
                        BottomlessBox = darkSouls.ReadEventFlag(Mappings.EventFlags.Common.BottomlessBox),
                        EstusFlask = darkSouls.ReadEventFlag(Mappings.EventFlags.Common.EstusFlask),
                        FirstSpell = darkSouls.ReadEventFlag(Mappings.EventFlags.Common.FirstSpell),
                        AbleToPayAbsolution = darkSouls.ReadEventFlag(Mappings.EventFlags.Common.AbleToPayAbsolution),
                        TitaniteShard = darkSouls.ReadEventFlag(Mappings.EventFlags.Common.TitaniteShard),
                        LargeTitaniteShard = darkSouls.ReadEventFlag(Mappings.EventFlags.Common.LargeTitaniteShard),
                        GreenTitaniteShard = darkSouls.ReadEventFlag(Mappings.EventFlags.Common.GreenTitaniteShard),
                        TitaniteChunk = darkSouls.ReadEventFlag(Mappings.EventFlags.Common.TitaniteChunk),
                        BlueTitaniteChunk = darkSouls.ReadEventFlag(Mappings.EventFlags.Common.BlueTitaniteChunk),
                        WhiteTitaniteChunk = darkSouls.ReadEventFlag(Mappings.EventFlags.Common.WhiteTitaniteChunk),
                        RedTitaniteChunk = darkSouls.ReadEventFlag(Mappings.EventFlags.Common.RedTitaniteChunk),
                        TitaniteSlab = darkSouls.ReadEventFlag(Mappings.EventFlags.Common.TitaniteSlab),
                        BlueTitaniteSlab = darkSouls.ReadEventFlag(Mappings.EventFlags.Common.BlueTitaniteSlab),
                        WhiteTitaniteSlab = darkSouls.ReadEventFlag(Mappings.EventFlags.Common.WhiteTitaniteSlab),
                        RedTitaniteSlab = darkSouls.ReadEventFlag(Mappings.EventFlags.Common.RedTitaniteSlab),
                        DragonScale = darkSouls.ReadEventFlag(Mappings.EventFlags.Common.DragonScale),
                        DemonTitanite = darkSouls.ReadEventFlag(Mappings.EventFlags.Common.DemonTitanite),
                        TwinklingTitanite = darkSouls.ReadEventFlag(Mappings.EventFlags.Common.TwinklingTitanite),

                        NoCovenant = darkSouls.ReadEventFlag(Mappings.EventFlags.Common.NoCovenant),
                        WayOfWhite = darkSouls.ReadEventFlag(Mappings.EventFlags.Common.WayOfWhite),
                        WarriorOfSunlight = darkSouls.ReadEventFlag(Mappings.EventFlags.Common.WarriorOfSunlight),
                        Darkwraith = darkSouls.ReadEventFlag(Mappings.EventFlags.Common.Darkwraith),
                        PathOfTheDragon = darkSouls.ReadEventFlag(Mappings.EventFlags.Common.PathOfTheDragon),
                        GravelordServant = darkSouls.ReadEventFlag(Mappings.EventFlags.Common.GravelordServant),
                        ForestHunter = darkSouls.ReadEventFlag(Mappings.EventFlags.Common.ForestHunter),
                        Darkmoon = darkSouls.ReadEventFlag(Mappings.EventFlags.Common.Darkmoon),
                        ChaosServant = darkSouls.ReadEventFlag(Mappings.EventFlags.Common.ChaosServant),
                    };

                    if (SavedStats.Char.LastBonfireId == -1 && Stats.Char.LastBonfireId != -1)
                    {
                        //Reload everything on new SaveGame Load
                        Stats.Events.AnorLondo = ReadAnorLondoData();
                        Stats.Events.Blighttown = ReadBlighttownData();
                        Stats.Events.Catacombs = ReadCatacombsData();
                        Stats.Events.ChasmOfTheAbyss = ReadChasmOfTheAbyssData();
                        Stats.Events.CrystalCave = ReadCrystalCaveData();
                        Stats.Events.DarkrootGarden = ReadDarkrootGardenData();
                        Stats.Events.DemonRuins = ReadDemonRuinsData();
                        Stats.Events.Depths = ReadDepthsData();
                        Stats.Events.FirelinkShrine = ReadFirelinkShrineData();
                        Stats.Events.KilnOfTheFirstFlame = ReadKilnOfTheFirstFlameData();
                        Stats.Events.LostIzalith = ReadLostIzalithData();
                        Stats.Events.NewLondoRuins = ReadNewLondoRuinsData();
                        Stats.Events.OolacileTownship = ReadOolacileTownshipData();
                        Stats.Events.PaintedWorld = ReadPaintedWorldData();
                        Stats.Events.QueelagsDomain = ReadQueelagsDomainData();
                        Stats.Events.RoyalWoods = ReadRoyalWoodsData();
                        Stats.Events.SanctuaryGarden = ReadSanctuaryGardenData();
                        Stats.Events.SensFortress = ReadSensFortressData();
                        Stats.Events.TombOfGiants = ReadTombOfGiantsData();
                        Stats.Events.UndeadAsylum = ReadUndeadAsylumData();
                        Stats.Events.UndeadBurg = ReadUndeadBurgData();
                        Stats.Events.UndeadParish = ReadUndeadParishData();
                        Stats.Events.ValleyOfDrakes = ReadValleyOfDrakesData();
                    } else {
                        //Reload only the Area where the char is
                        switch(Stats.Char.AreaId) {
                            case 1510:
                            case 2002:
                            case 2003: //Chamber of the Princess
                            case 2004: //Darkmoon Tomb
                                Stats.Events.AnorLondo = ReadAnorLondoData();
                                break;
                            case 1400:
                                Stats.Events.Blighttown = ReadBlighttownData();
                                break;
                            case 1300:
                            case 2020:
                                Stats.Events.Catacombs = ReadCatacombsData();
                                break;
                            case 1214:
                            case 2012:
                                Stats.Events.ChasmOfTheAbyss = ReadChasmOfTheAbyssData();
                                break;
                            case 1701:
                            case 2019:
                                Stats.Events.CrystalCave = ReadCrystalCaveData();
                                break;
                            case 1200:
                                Stats.Events.DarkrootGarden = ReadDarkrootGardenData();
                                break;
                            case 1410:
                                Stats.Events.DemonRuins = ReadDemonRuinsData();
                                break;
                            case 1000:
                            case 2014:
                                Stats.Events.Depths = ReadDepthsData();
                                break;
                            case 1020:
                            case 2000:
                                Stats.Events.FirelinkShrine = ReadFirelinkShrineData();
                                break;
                            case 1800:
                            case 1801:
                                Stats.Events.KilnOfTheFirstFlame = ReadKilnOfTheFirstFlameData();
                                break;
                            case 1411:
                                Stats.Events.LostIzalith = ReadLostIzalithData();
                                break;
                            case 1600:
                            case 1610: //Abyss
                            case 2005: //Abyss
                                Stats.Events.NewLondoRuins = ReadNewLondoRuinsData();
                                break;
                            case 1213:
                            case 2011:
                            case 2013: //Dungeon
                                Stats.Events.OolacileTownship = ReadOolacileTownshipData();
                                break;
                            case 1100:
                            case 2016:
                                Stats.Events.PaintedWorld = ReadPaintedWorldData();
                                break;
                            case 1401:
                            case 2006: //Daughter of Chaos
                                Stats.Events.QueelagsDomain = ReadQueelagsDomainData();
                                break;
                            case 1212:
                                Stats.Events.RoyalWoods = ReadRoyalWoodsData();
                                break;
                            case 1210:
                            case 2009:
                                Stats.Events.SanctuaryGarden = ReadSanctuaryGardenData();
                                break;
                            case 1500:
                                Stats.Events.SensFortress = ReadSensFortressData();
                                break;
                            case 1310:
                            case 2007: //Gravelord Altar
                            case 2017:
                                Stats.Events.TombOfGiants = ReadTombOfGiantsData();
                                break;
                            case 1810:
                                Stats.Events.UndeadAsylum = ReadUndeadAsylumData();
                                break;
                            case 1010:
                                Stats.Events.UndeadBurg = ReadUndeadBurgData();
                                break;
                            case 1011:
                            case 2001: //Sunlight Altar
                            case 2015:
                                Stats.Events.UndeadParish = ReadUndeadParishData();
                                break;
                            case 1602:
                                Stats.Events.ValleyOfDrakes = ReadValleyOfDrakesData();
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
                    if (SavedStats.Loaded && (ResettedStats == null || SavedStats.Char.Clock < ResettedStats.Clock || SavedStats.Char.SaveSlot != ResettedStats.SaveSlot || SavedStats.Char.CharacterName != ResettedStats.CharacterName))
                    {
                        ResettedStats = FileService.LoadResettedData(SavedStats);
                    }

                    //TODO Save when resettable event flags change
                    //TODO replace Event Flag Values for reseted event flags
                } catch { }

                try
                {
                    if(_settings.CompareEventFlags)
                    {
                        FileService.WriteEventLog(SavedStats);
                    }
                    CommunicationService.SendMessage(JsonConvert.SerializeObject(SavedStats), false);
                }
                catch {}
            }
        }

#region ReadEventFlags

        public static AnorLondoEvents<bool> ReadAnorLondoData()
        {
            return new() {
                MainHallDoor = darkSouls.ReadEventFlag(Mappings.EventFlags.AnorLondo.MainHallDoor),
                BlacksmithShortcutDoor = darkSouls.ReadEventFlag(Mappings.EventFlags.AnorLondo.BlacksmithShortcutDoor),
                SolaireBonfireShortcutDoor = darkSouls.ReadEventFlag(Mappings.EventFlags.AnorLondo.SolaireBonfireShortcutDoor),
                Dark = darkSouls.ReadEventFlag(Mappings.EventFlags.AnorLondo.Dark),
                Gwyndolin = darkSouls.ReadEventFlag(Mappings.EventFlags.AnorLondo.Gwyndolin),
                OrnsteinSmough = darkSouls.ReadEventFlag(Mappings.EventFlags.AnorLondo.OrnsteinSmough),
            };
        }

        public static BlighttownEvents<bool> ReadBlighttownData()
        {
            return new() {
                ChaosWitchQueelag = darkSouls.ReadEventFlag(Mappings.EventFlags.Blighttown.ChaosWitchQueelag),
            };
        }

        public static CatacombsEvents<bool> ReadCatacombsData()
        {
            return new() {
                Pinwheel = darkSouls.ReadEventFlag(Mappings.EventFlags.Catacombs.Pinwheel),
            };
        }

        public static ChasmOfTheAbyssEvents<bool> ReadChasmOfTheAbyssData()
        {
            return new() {
                Manus = darkSouls.ReadEventFlag(Mappings.EventFlags.ChasmOfTheAbyss.Manus),
            };
        }

        public static CrystalCaveEvents<bool> ReadCrystalCaveData()
        {
            return new() {
                Seath = darkSouls.ReadEventFlag(Mappings.EventFlags.CrystalCave.Seath),
            };
        }

        public static DarkrootGardenEvents<bool> ReadDarkrootGardenData()
        {
            return new() {
                MoonlightButterfly = darkSouls.ReadEventFlag(Mappings.EventFlags.DarkrootGarden.MoonlightButterfly),
                CatCovenantRing = darkSouls.ReadEventFlag(Mappings.EventFlags.DarkrootGarden.CatCovenantRing),
                Sif = darkSouls.ReadEventFlag(Mappings.EventFlags.DarkrootGarden.Sif),
            };
        }

        public static DemonRuinsEvents<bool> ReadDemonRuinsData()
        {
            return new() {
                QueelagsDomainElevator = darkSouls.ReadEventFlag(Mappings.EventFlags.DemonRuins.QueelagsDomainElevator),
                DemonFiresage = darkSouls.ReadEventFlag(Mappings.EventFlags.DemonRuins.DemonFiresage),
                CeaselessDischarge = darkSouls.ReadEventFlag(Mappings.EventFlags.DemonRuins.CeaselessDischarge),
                CentipedeDemon = darkSouls.ReadEventFlag(Mappings.EventFlags.DemonRuins.CentipedeDemon),
            };
        }

        public static DepthsEvents<bool> ReadDepthsData()
        {
            return new() {
                BlighttownDoor = darkSouls.ReadEventFlag(Mappings.EventFlags.Depths.BlighttownDoor),
                GapingDragon = darkSouls.ReadEventFlag(Mappings.EventFlags.Depths.GapingDragon),
            };
        }

        public static FirelinkShrineEvents<bool> ReadFirelinkShrineData()
        {
            return new() {
                TalismanChest = darkSouls.ReadEventFlag(Mappings.EventFlags.FirelinkShrine.TalismanChest),
                HomewardBoneChest = darkSouls.ReadEventFlag(Mappings.EventFlags.FirelinkShrine.HomewardBoneChest),
                CrackedRedEyeOrbChest = darkSouls.ReadEventFlag(Mappings.EventFlags.FirelinkShrine.CrackedRedEyeOrbChest),
                LloydTalismanChest = darkSouls.ReadEventFlag(Mappings.EventFlags.FirelinkShrine.LloydTalismanChest),
                CurledUpLikeABall = darkSouls.ReadEventFlag(Mappings.EventFlags.FirelinkShrine.CurledUpLikeABall),
                PyromancyFlame = darkSouls.ReadEventFlag(Mappings.EventFlags.FirelinkShrine.PyromancyFlame),
                PetrusCopperCoin = darkSouls.ReadEventFlag(Mappings.EventFlags.FirelinkShrine.PetrusCopperCoin),
            };
        }

        public static KilnOfTheFirstFlameEvents<bool> ReadKilnOfTheFirstFlameData()
        {
            return new() {
                Gwyn = darkSouls.ReadEventFlag(Mappings.EventFlags.KilnOfTheFirstFlame.Gwyn),
            };
        }

        public static LostIzalithEvents<bool> ReadLostIzalithData()
        {
            return new() {
                BedOfChaos = darkSouls.ReadEventFlag(Mappings.EventFlags.LostIzalith.BedOfChaos),
            };
        }

        public static NewLondoRuinsEvents<bool> ReadNewLondoRuinsData()
        {
            return new() {
                Drained = darkSouls.ReadEventFlag(Mappings.EventFlags.NewLondoRuins.Drained),
                DoorToTheSeal = darkSouls.ReadEventFlag(Mappings.EventFlags.NewLondoRuins.DoorToTheSeal),
                ValleyOfDrakesDoor = darkSouls.ReadEventFlag(Mappings.EventFlags.NewLondoRuins.ValleyOfDrakesDoor),
                ShortcutLadder = darkSouls.ReadEventFlag(Mappings.EventFlags.NewLondoRuins.ShortcutLadder),
                FirelinkElevator = darkSouls.ReadEventFlag(Mappings.EventFlags.NewLondoRuins.FirelinkElevator),
                EstocCorpse = darkSouls.ReadEventFlag(Mappings.EventFlags.NewLondoRuins.EstocCorpse),
                FourKings = darkSouls.ReadEventFlag(Mappings.EventFlags.NewLondoRuins.FourKings),
            };
        }

        public static OolacileTownshipEvents<bool> ReadOolacileTownshipData()
        {
            return new() {
                CrestKeyDoor = darkSouls.ReadEventFlag(Mappings.EventFlags.OolacileTownship.CrestKeyDoor),
            };
        }

        public static PaintedWorldEvents<bool> ReadPaintedWorldData()
        {
            return new() {
                Priscilla = darkSouls.ReadEventFlag(Mappings.EventFlags.PaintedWorld.Priscilla),
            };
        }

        public static QueelagsDomainEvents<bool> ReadQueelagsDomainData()
        {
            return new() {
                BellOfAwakening = darkSouls.ReadEventFlag(Mappings.EventFlags.QueelagsDomain.BellOfAwakening),
            };
        }

        public static RoyalWoodsEvents<bool> ReadRoyalWoodsData()
        {
            return new() {
                Kalameet = darkSouls.ReadEventFlag(Mappings.EventFlags.RoyalWoods.Kalameet),
                Artorias = darkSouls.ReadEventFlag(Mappings.EventFlags.RoyalWoods.Artorias),
            };
        }

        public static SanctuaryGardenEvents<bool> ReadSanctuaryGardenData()
        {
            return new() {
                SanctuaryGuardian = darkSouls.ReadEventFlag(Mappings.EventFlags.SanctuaryGarden.SanctuaryGuardian),
            };
        }

        public static SensFortressEvents<bool> ReadSensFortressData()
        {
            return new() {
                CageElevator = darkSouls.ReadEventFlag(Mappings.EventFlags.SensFortress.CageElevator),
                MainGate = darkSouls.ReadEventFlag(Mappings.EventFlags.SensFortress.MainGate),
                IronGolem = darkSouls.ReadEventFlag(Mappings.EventFlags.SensFortress.IronGolem),
            };
        }

        public static TombOfGiantsEvents<bool> ReadTombOfGiantsData()
        {
            return new() {
                Nito = darkSouls.ReadEventFlag(Mappings.EventFlags.TombOfGiants.Nito),
            };
        }

        public static UndeadAsylumEvents<bool> ReadUndeadAsylumData()
        {
            return new() {
                AsylumDeamon = darkSouls.ReadEventFlag(Mappings.EventFlags.UndeadAsylum.AsylumDeamon),
                PreOscarFog = darkSouls.ReadEventFlag(Mappings.EventFlags.UndeadAsylum.PreOscarFog),
                CellDoor = darkSouls.ReadEventFlag(Mappings.EventFlags.UndeadAsylum.CellDoor),
                F2WestDoor = darkSouls.ReadEventFlag(Mappings.EventFlags.UndeadAsylum.F2WestDoor),
                ShortcutDoor = darkSouls.ReadEventFlag(Mappings.EventFlags.UndeadAsylum.ShortcutDoor),
                F2EastDoor = darkSouls.ReadEventFlag(Mappings.EventFlags.UndeadAsylum.F2EastDoor),
                BigPilgrimDoor = darkSouls.ReadEventFlag(Mappings.EventFlags.UndeadAsylum.BigPilgrimDoor),
                OscarTrap = darkSouls.ReadEventFlag(Mappings.EventFlags.UndeadAsylum.OscarTrap),
                OscarGiftEstus = darkSouls.ReadEventFlag(Mappings.EventFlags.UndeadAsylum.OscarGiftEstus),
                OscarGiftF2EastKey = darkSouls.ReadEventFlag(Mappings.EventFlags.UndeadAsylum.OscarGiftF2EastKey),
                BossDoor = darkSouls.ReadEventFlag(Mappings.EventFlags.UndeadAsylum.BossDoor),
            };
        }

        public static UndeadBurgEvents<bool> ReadUndeadBurgData()
        {
            return new() {
                TaurusDeamon = darkSouls.ReadEventFlag(Mappings.EventFlags.UndeadBurg.TaurusDeamon),
                CapraDeamon = darkSouls.ReadEventFlag(Mappings.EventFlags.UndeadBurg.CapraDeamon),
                BlackKnight = darkSouls.ReadEventFlag(Mappings.EventFlags.UndeadBurg.BlackKnight),
                HellkiteFirst = darkSouls.ReadEventFlag(Mappings.EventFlags.UndeadBurg.HellkiteFirst),
                HellkiteSecond = darkSouls.ReadEventFlag(Mappings.EventFlags.UndeadBurg.HellkiteSecond),
                FogGate = darkSouls.ReadEventFlag(Mappings.EventFlags.UndeadBurg.FogGate),
                ShortcutLadder = darkSouls.ReadEventFlag(Mappings.EventFlags.UndeadBurg.ShortcutLadder),
                FlamingBarrel = darkSouls.ReadEventFlag(Mappings.EventFlags.UndeadBurg.FlamingBarrel),
                LowerBurgShortcut = darkSouls.ReadEventFlag(Mappings.EventFlags.UndeadBurg.LowerBurgShortcut),
                BasementKeyDoor = darkSouls.ReadEventFlag(Mappings.EventFlags.UndeadBurg.BasementKeyDoor),
                WatchtowerUpperDoor = darkSouls.ReadEventFlag(Mappings.EventFlags.UndeadBurg.WatchtowerUpperDoor),
                WatchtowerLowerDoor = darkSouls.ReadEventFlag(Mappings.EventFlags.UndeadBurg.WatchtowerLowerDoor),
                SublightALtasGate = darkSouls.ReadEventFlag(Mappings.EventFlags.UndeadBurg.SublightALtasGate),
                BlackFirebombChest = darkSouls.ReadEventFlag(Mappings.EventFlags.UndeadBurg.BlackFirebombChest),
                GoldPineResinChest = darkSouls.ReadEventFlag(Mappings.EventFlags.UndeadBurg.GoldPineResinChest),
            };
        }

        public static UndeadParishEvents<bool> ReadUndeadParishData()
        {
            return new() {
                FirelinkElevator = darkSouls.ReadEventFlag(Mappings.EventFlags.UndeadParish.FirelinkElevator),
                AltarOfSunlight = darkSouls.ReadEventFlag(Mappings.EventFlags.UndeadParish.AltarOfSunlight),
                BerenikeKnight = darkSouls.ReadEventFlag(Mappings.EventFlags.UndeadParish.BerenikeKnight),
                Channeler = darkSouls.ReadEventFlag(Mappings.EventFlags.UndeadParish.Channeler),
                BellOfAwakening = darkSouls.ReadEventFlag(Mappings.EventFlags.UndeadParish.BellOfAwakening),
                LightningSpear = darkSouls.ReadEventFlag(Mappings.EventFlags.UndeadParish.LightningSpear),
                BellGargoyles = darkSouls.ReadEventFlag(Mappings.EventFlags.UndeadParish.BellGargoyles),
            };
        }

        public static ValleyOfDrakesEvents<bool> ReadValleyOfDrakesData()
        {
            return new() {
                DarkrootElevator = darkSouls.ReadEventFlag(Mappings.EventFlags.ValleyOfDrakes.DarkrootElevator),
                DarkrootElevatorInoperable = darkSouls.ReadEventFlag(Mappings.EventFlags.ValleyOfDrakes.DarkrootElevatorInoperable),
                KeyToTheSeal = darkSouls.ReadEventFlag(Mappings.EventFlags.ValleyOfDrakes.KeyToTheSeal),
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
