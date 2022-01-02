namespace DarkSoulsOBSOverlay.Models
{
    public partial class DarkSoulsData
    {
        public Settings Settings { get; set; } = null;
        public bool Connected { get; set; } = false;
        public bool Loaded { get; set; } = false;
        public string Version { get; set; } = null;
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
        public double Clock { get; set; } = 0;
        public UndeadAsylumData UndeadAsylum { get; set; }
        public FirelinkShrineData FirelinkShrine { get; set; }
    }

    public class UndeadAsylumData
    {
        public bool PreOscarFog { get; set; } = false;
        public bool CellDoor { get; set; } = false;
        public bool F2WestDoor { get; set; } = false;
        public bool ShortcutDoor { get; set; } = false;
        public bool F2EastDoor { get; set; } = false;
        public bool BigPilgrimDoor { get; set; } = false;
        public bool OscarTrapSprung { get; set; } = false;
        public bool AsylumDeamon { get; set; } = false;
    }

    public class FirelinkShrineData
    {
        public bool TalismanChest { get; set; } = false;
        public bool HomewardBoneChest { get; set; } = false;
        public bool CrackedRedEyeOrbChest { get; set; } = false;
        public bool LloydTalismanChest { get; set; } = false;
    }
}
