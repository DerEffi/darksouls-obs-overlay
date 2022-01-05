using System.Collections.Generic;

namespace DarkSoulsOBSOverlay.Models
{
    public class DarkSoulsData
    {
        public Settings Settings { get; set; } = new();
        public bool Connected { get; set; } = false;
        public bool Loaded { get; set; } = false;
        public string Version { get; set; } = null;
        public double Clock { get; set; } = 0;
        public List<KeyValuePair<int, int>> UpdatedEventFlags { get; set; } = new();
        public CharData Char { get; set; } = new();
        
        //EventFlags
        public CommonData Common { get; set; } = new();
        public AnorLondoData AnorLondo { get; set; } = new();
        public BlighttownData Blighttown { get; set; } = new();
        public CatacombsData Catacombs { get; set; } = new();
        public ChasmOfTheAbyssData ChasmOfTheAbyss { get; set; } = new();
        public CrystalCaveData CrystalCave { get; set; } = new();
        public DarkrootGardenData DarkrootGarden { get; set; } = new();
        public DemonRuinsData DemonRuins { get; set; } = new();
        public DepthsData Depths { get; set; } = new();
        public FirelinkShrineData FirelinkShrine { get; set; } = new();
        public KilnOfTheFirstFlameData KilnOfTheFirstFlame { get; set; } = new();
        public LostIzalithData LostIzalith { get; set; } = new();
        public NewLondoRuinsData NewLondoRuins { get; set; } = new();
        public OolacileTownshipData OolacileTownship { get; set; } = new();
        public PaintedWorldData PaintedWorld { get; set; } = new();
        public QueelagsDomainData QueelagsDomain { get; set; } = new();
        public RoyalWoodsData RoyalWoods { get; set; } = new();
        public SanctuaryGardenData SanctuaryGarden { get; set; } = new();
        public SensFortressData SensFortress { get; set; } = new();
        public TombOfGiantsData TombOfGiants { get; set; } = new();
        public UndeadAsylumData UndeadAsylum { get; set; } = new();
        public UndeadBurgData UndeadBurg { get; set; } = new();
        public UndeadParishData UndeadParish { get; set; } = new();
        public ValleyOfDrakesData ValleyOfDrakes { get; set; } = new();
    }

    public class CharData
    {
        public string Area { get; set; } = null;
        public int AreaId { get; set; } = 0;
        public string CharacterName { get; set; } = null;
        public string CharacterClass { get; set; } = null;
        public int CharacterClassId { get; set; } = 0;
        public string LastBonfire { get; set; } = null;
        public int LastBonfireId { get; set; } = 0;
        public int Health { get; set; } = 0;
        public int HealthMax { get; set; } = 0;
        public int Deaths { get; set; } = 0;
    }

    public class CommonData
    {
        public bool WeaponSmithbox { get; set; } = false; //Obtained
        public bool ArmorSmithbox { get; set; } = false; //Obtained
        public bool Repairbox { get; set; } = false; //Obtained
        public bool BottomlessBox { get; set; } = false; //Obtained
        public bool EstusFlask { get; set; } = false; //Obtained
        public bool FirstSpell { get; set; } = false; //Obtained
        public bool AbleToPayAbsolution { get; set; } = false; //Triggered
        public bool TitaniteShard { get; set; } = false; //Obtained
        public bool LargeTitaniteShard { get; set; } = false; //Obtained
        public bool GreenTitaniteShard { get; set; } = false; //Obtained
        public bool TitaniteChunk { get; set; } = false; //Obtained
        public bool BlueTitaniteChunk { get; set; } = false; //Obtained
        public bool WhiteTitaniteChunk { get; set; } = false; //Obtained
        public bool RedTitaniteChunk { get; set; } = false; //Obtained
        public bool TitaniteSlab { get; set; } = false; //Obtained
        public bool BlueTitaniteSlab { get; set; } = false; //Obtained
        public bool WhiteTitaniteSlab { get; set; } = false; //Obtained
        public bool RedTitaniteSlab { get; set; } = false; //Obtained
        public bool DragonScale { get; set; } = false; //Obtained
        public bool DemonTitanite { get; set; } = false; //Obtained
        public bool TwinklingTitanite { get; set; } = false; //Obtained

        public bool NoCovenant { get; set; } = false; //covenant
        public bool WayOfWhite { get; set; } = false; //covenant
        public bool WarriorOfSunlight { get; set; } = false; //covenant
        public bool Darkwraith { get; set; } = false; //covenant
        public bool PathOfTheDragon { get; set; } = false; //covenant
        public bool GravelordServant { get; set; } = false; //covenant
        public bool ForestHunter { get; set; } = false; //covenant
        public bool Darkmoon { get; set; } = false; //covenant
        public bool ChaosServant { get; set; } = false; //covenant
    }

    public class AnorLondoData
    {
        public bool MainHallDoor { get; set; } = false; //opened
        public bool BlacksmithShortcutDoor { get; set; } = false; //opened
        public bool SolaireBonfireShortcutDoor { get; set; } = false; //opened
        public bool Dark { get; set; } = false; //dark
        public bool Gwyndolin { get; set; } = false; //defeated
        public bool OrnsteinSmough { get; set; } = false; //defeated
    }

    public class BlighttownData
    {
        public bool ChaosWitchQueelag { get; set; } = false; //defeated
    }

    public class CatacombsData
    {
        public bool Pinwheel { get; set; } = false; //defeated
    }

    public class ChasmOfTheAbyssData
    {
        public bool Manus { get; set; } = false; //defeated
    }

    public class CrystalCaveData
    {
        public bool Seath { get; set; } = false; //defeated
    }

    public class DarkrootGardenData
    {
        public bool MoonlightButterfly { get; set; } = false; //defeated
        public bool CatCovenantRing { get; set; } = false; //gifted
        public bool Sif { get; set; } = false; //defeated
    }

    public class DemonRuinsData
    {
        public bool QueelagsDomainElevator { get; set; } = false; //operatable
        public bool DemonFiresage { get; set; } = false; //defeated
        public bool CeaselessDischarge { get; set; } = false; //defeated
        public bool CentipedeDemon { get; set; } = false; //defeated
    }

    public class DepthsData
    {
        public bool BlighttownDoor { get; set; } = false; //opened
        public bool GapingDragon { get; set; } = false; //defeated
    }

    public class FirelinkShrineData
    {
        public bool TalismanChest { get; set; } = false; //opened
        public bool HomewardBoneChest { get; set; } = false; //opened
        public bool CrackedRedEyeOrbChest { get; set; } = false; //opened
        public bool LloydTalismanChest { get; set; } = false; //opened
        public bool CurledUpLikeABall { get; set; } = false; //?
        public bool PyromancyFlame { get; set; } = false; //gifted
        public bool PetrusCopperCoin { get; set; } = false; //gifted
    }

    public class KilnOfTheFirstFlameData
    {
        public bool Gwyn { get; set; } = false; //defeated
    }

    public class LostIzalithData
    {
        public bool BedOfChaos { get; set; } = false; //defeated
    }

    public class NewLondoRuinsData
    {
        public bool Drained { get; set; } = false; //drained
        public bool DoorToTheSeal { get; set; } = false; //opened
        public bool ValleyOfDrakesDoor { get; set; } = false; //opened
        public bool ShortcutLadder { get; set; } = false; //operable
        public bool FirelinkElevator { get; set; } = false; //operable
        public bool EstocCorpse { get; set; } = false; //collapsed
        public bool FourKings { get; set; } = false; //defeated
    }

    public class OolacileTownshipData
    {
        public bool CrestKeyDoor { get; set; } = false; //opened
    }

    public class PaintedWorldData
    {
        public bool Priscilla { get; set; } = false; //defeated
    }

    public class QueelagsDomainData
    {
        public bool BellOfAwakening { get; set; } = false;
    }

    public class RoyalWoodsData
    {
        public bool Kalameet { get; set; } = false; //defeated
        public bool Artorias { get; set; } = false; //defeated
    }

    public class SanctuaryGardenData
    {
        public bool SanctuaryGuardian { get; set; } = false; //defeated
    }

    public class SensFortressData
    {
        public bool CageElevator { get; set; } = false; //operatable
        public bool MainGate { get; set; } = false; //open
        public bool IronGolem { get; set; } = false; //defeated
    }

    public class TombOfGiantsData
    {
        public bool Nito { get; set; } = false; //defeated
    }

    public class UndeadAsylumData
    {
        public bool AsylumDeamon { get; set; } = false; //defeated
        public bool PreOscarFog { get; set; } = false; //cleared
        public bool CellDoor { get; set; } = false; //opened
        public bool F2WestDoor { get; set; } = false; //opened
        public bool ShortcutDoor { get; set; } = false; //opened
        public bool F2EastDoor { get; set; } = false; //opened
        public bool BigPilgrimDoor { get; set; } = false; //opened
        public bool OscarTrap { get; set; } = false; //sprung
        public bool OscarGiftEstus { get; set; } = false; //obtained
        public bool OscarGiftF2EastKey { get; set; } = false; //obtained
        public bool BossDoor { get; set; } = false; //opened
    }

    public class UndeadBurgData
    {
        public bool TaurusDeamon { get; set; } = false; //defeated
        public bool CapraDeamon { get; set; } = false; //defeated
        public bool BlackKnight { get; set; } = false; //defeated
        public bool HellkiteFirst { get; set; } = false; //defeated
        public bool HellkiteSecond { get; set; } = false; //defeated
        public bool FogGate { get; set; } = false; //opened
        public bool ShortcutLadder { get; set; } = false; //operatable
        public bool FlamingBarrel { get; set; } = false; //kicked down
        public bool LowerBurgShortcut { get; set; } = false; //opened
        public bool BasementKeyDoor { get; set; } = false; //opened
        public bool WatchtowerUpperDoor { get; set; } = false; //opened
        public bool WatchtowerLowerDoor { get; set; } = false; //opened
        public bool SublightALtasGate { get; set; } = false; //opened
        public bool BlackFirebombChest { get; set; } = false; //opened
        public bool GoldPineResinChest { get; set; } = false; //opened
    }

    public class UndeadParishData
    {
        public bool FirelinkElevator { get; set; } = false; //operatable
        public bool AltarOfSunlight { get; set; } = false; //prayed
        public bool BerenikeKnight { get; set; } = false; //defeated
        public bool Channeler { get; set; } = false; //defeated
        public bool BellOfAwakening { get; set; } = false; //ringed
        public bool LightningSpear { get; set; } = false; //obtained
        public bool BellGargoyles { get; set; } = false; //defeated
    }

    public class ValleyOfDrakesData
    {
        public bool DarkrootElevator { get; set; } = false; //inoperable
        public bool DarkrootElevatorInoperable { get; set; } = false; //inoperable
        public bool KeyToTheSeal { get; set; } = false; //obtained
    }
}
