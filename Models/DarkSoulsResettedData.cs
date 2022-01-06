using System.Collections.Generic;

namespace DarkSoulsOBSOverlay.Models
{
    public class DarkSoulsResettedData
    {
        public double Clock { get; set; } = -1;
        public string CharacterName { get; set; } = null;
        public int SaveSlot { get; set; } = -1;
        public List<int> EventFlags { get; set; } = new();
    }
}
