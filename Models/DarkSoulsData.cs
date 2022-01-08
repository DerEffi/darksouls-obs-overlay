using DarkSoulsOBSOverlay.Models.Mappings;
using System.Collections.Generic;

namespace DarkSoulsOBSOverlay.Models
{
    public class DarkSoulsData
    {
        public Settings Settings { get; set; } = new();
        public bool Connected { get; set; } = false;
        public bool Loaded { get; set; } = false;
        public string Version { get; set; } = null;
        public List<KeyValuePair<int, int>> UpdatedEventFlags { get; set; } = new();
        public CharData Char { get; set; } = new();
        public EventFlags<bool> Events { get; set; } = new();
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
        public double Clock { get; set; } = 0;
        public int SaveSlot { get; set; } = 0;
    }
}
