using DarkSoulsOBSOverlay.Models.Events;

namespace DarkSoulsOBSOverlay.Models.Mappings
{
    public partial class Mappings
    {
        public static readonly EventFlags<int> EventFlags = new()
        {
            Common = {
                WeaponSmithbox = 250,
                ArmorSmithbox = 251,
                Repairbox = 252,
                BottomlessBox = 258,
                EstusFlask = 716,
                FirstSpell = 719,
                AbleToPayAbsolution = 744,
                TitaniteShard = 780,
                LargeTitaniteShard = 781,
                GreenTitaniteShard = 782,
                TitaniteChunk = 783,
                BlueTitaniteChunk = 784,
                WhiteTitaniteChunk = 785,
                RedTitaniteChunk = 786,
                TitaniteSlab = 787,
                BlueTitaniteSlab = 788,
                WhiteTitaniteSlab = 789,
                RedTitaniteSlab = 790,
                DragonScale = 791,
                DemonTitanite = 792,
                TwinklingTitanite = 793,
                NoCovenant = 850,
                WayOfWhite = 851,
                WarriorOfSunlight = 853,
                Darkwraith = 854,
                PathOfTheDragon = 855,
                GravelordServant = 856,
                ForestHunter = 857,
                Darkmoon = 858,
                ChaosServant = 859,
            },
            AltarOfSunlight = {
            },
            AnorLondo = {
                MainHallDoor = 11510200,
                BlacksmithShortcutDoor = 11510210,
                SolaireBonfireShortcutDoor = 11510251,
                Dark = 11510272,
                Gwyndolin = 11510900,
                OrnsteinSmough = 12,
            },
            AshLake = {
            },
            BattleOfStoicism = {
            },
            Blighttown = {
                ChaosWitchQueelag = 9,
            },
            Catacombs = {
                Pinwheel = 6,
            },
            ChasmOfTheAbyss = {
                Manus = 11210002,
            },
            CrystalCave = {
                Seath = 14,
            },
            DarkrootBasin = {
            },
            DarkrootGarden = {
                MoonlightButterfly = 11200900,
                CatCovenantRing = 11200592,
                Sif = 5,
            },
            DemonRuins = {
                QueelagsDomainElevator = 11410401,
                DemonFiresage = 11410410,
                CeaselessDischarge = 11410900,
                CentipedeDemon = 11410901,
            },
            Depths = {
                BlighttownDoor = 11000111,
                GapingDragon = 2,
            },
            DukesArchives = {
            },
            FirelinkShrine = {
                TalismanChest = 11020701,
                HomewardBoneChest = 11020702,
                CrackedRedEyeOrbChest = 11020703,
                LloydTalismanChest = 11020700,
                CurledUpLikeABall = 11025060,
                PyromancyFlame = 11020602,
                PetrusCopperCoin = 11020599,
            },
            GreatHollow = {
            },
            KilnOfTheFirstFlame = {
                Gwyn = 15,
            },
            LostIzalith = {
                BedOfChaos = 10,
            },
            NewLondoRuins = {
                Drained = 11600100,
                DoorToTheSeal = 11600110,
                ValleyOfDrakesDoor = 11600120,
                ShortcutLadder = 11600160,
                FirelinkElevator = 11600201,
                EstocCorpse = 11600650,
                FourKings = 13,
            },
            OolacileSanctuary = {
            },
            OolacileTownship = {
                CrestKeyDoor = 11210650,
            },
            PaintedWorld = {
                Priscilla = 4,
            },
            QueelagsDomain = {
                BellOfAwakening = 11400200,
            },
            RoyalWoods = {
                Kalameet = 11210004,
                Artorias = 11210001,
            },
            SanctuaryGarden = {
                SanctuaryGuardian = 11210000,
            },
            SensFortress = {
                CageElevator = 11500101,
                MainGate = 11500200,
                IronGolem = 11,
            },
            TombOfGiants = {
                Nito = 7,
            },
            UndeadAsylum = {
                AsylumDeamon = 16,
                PreOscarFog = 11810090,
                CellDoor = 11810103,
                F2WestDoor = 11810104,
                ShortcutDoor = 11810105,
                F2EastDoor = 11810106,
                BigPilgrimDoor = 11810110,
                OscarTrap = 11810211,
                OscarGiftEstus = 11810590,
                OscarGiftF2EastKey = 11810591,
                BossDoor = 11810112,
            },
            UndeadBurg = {
                TaurusDeamon = 11010901,
                CapraDeamon = 11010902,
                BlackKnight = 11010861,
                HellkiteFirst = 11010790,
                HellkiteSecond = 11010791,
                FogGate = 11010090,
                ShortcutLadder = 11010100,
                FlamingBarrel = 11010120,
                LowerBurgShortcut = 11010160,
                BasementKeyDoor = 11010172,
                WatchtowerUpperDoor = 11010191,
                WatchtowerLowerDoor = 11010192,
                SublightALtasGate = 11010621,
                BlackFirebombChest = 11010650,
                GoldPineResinChest = 11010651,
            },
            UndeadParish = {
                FirelinkElevator = 11020302,
                AltarOfSunlight = 11015031,
                BerenikeKnight = 11010863,
                Channeler = 11010865,
                BellOfAwakening = 11010700,
                LightningSpear = 11010594,
                BellGargoyles = 3,
            },
            ValleyOfDrakes = {
                DarkrootElevator = 11600231,
                DarkrootElevatorInoperable = 11605123,
                KeyToTheSeal = 11600592,
            },
        };
    }

    public class EventFlags<T>
    {
        public CommonEvents<T> Common { get; set; } = new();
        public AltarOfSunlightEvents<T> AltarOfSunlight { get; set; } = new();
        public AnorLondoEvents<T> AnorLondo { get; set; } = new();
        public AshLakeEvents<T> AshLake { get; set; } = new();
        public BattleOfStoicismEvents<T> BattleOfStoicism { get; set; } = new();
        public BlighttownEvents<T> Blighttown { get; set; } = new();
        public CatacombsEvents<T> Catacombs { get; set; } = new();
        public ChasmOfTheAbyssEvents<T> ChasmOfTheAbyss { get; set; } = new();
        public CrystalCaveEvents<T> CrystalCave { get; set; } = new();
        public DarkrootBasinEvents<T> DarkrootBasin { get; set; } = new();
        public DarkrootGardenEvents<T> DarkrootGarden { get; set; } = new();
        public DemonRuinsEvents<T> DemonRuins { get; set; } = new();
        public DepthsEvents<T> Depths { get; set; } = new();
        public DukesArchivesEvents<T> DukesArchives { get; set; } = new();
        public FirelinkShrineEvents<T> FirelinkShrine { get; set; } = new();
        public GreatHollowEvents<T> GreatHollow { get; set; } = new();
        public KilnOfTheFirstFlameEvents<T> KilnOfTheFirstFlame { get; set; } = new();
        public LostIzalithEvents<T> LostIzalith { get; set; } = new();
        public NewLondoRuinsEvents<T> NewLondoRuins { get; set; } = new();
        public OolacileSanctuaryEvents<T> OolacileSanctuary { get; set; } = new();
        public OolacileTownshipEvents<T> OolacileTownship { get; set; } = new();
        public PaintedWorldEvents<T> PaintedWorld { get; set; } = new();
        public QueelagsDomainEvents<T> QueelagsDomain { get; set; } = new();
        public RoyalWoodsEvents<T> RoyalWoods { get; set; } = new();
        public SanctuaryGardenEvents<T> SanctuaryGarden { get; set; } = new();
        public SensFortressEvents<T> SensFortress { get; set; } = new();
        public TombOfGiantsEvents<T> TombOfGiants { get; set; } = new();
        public UndeadAsylumEvents<T> UndeadAsylum { get; set; } = new();
        public UndeadBurgEvents<T> UndeadBurg { get; set; } = new();
        public UndeadParishEvents<T> UndeadParish { get; set; } = new();
        public ValleyOfDrakesEvents<T> ValleyOfDrakes { get; set; } = new();
    }
}
