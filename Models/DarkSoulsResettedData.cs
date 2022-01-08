namespace DarkSoulsOBSOverlay.Models
{
    public class DarkSoulsResettedData
    {
        public double Clock { get; set; } = -1;
        public string CharacterName { get; set; } = null;
        public int SaveSlot { get; set; } = -1;
        public ResettedEventFlags EventFlags { get; set; } = new();
    }

    public class ResettedEventFlags
    {

    }
}
