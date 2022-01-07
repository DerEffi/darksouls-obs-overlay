using Newtonsoft.Json;

namespace DarkSoulsOBSOverlay.Models
{
    public class Settings
    {
        public int UpdateInterval { get; set; } = 1;
        public bool CompareEventFlags { get; set; } = false;
    }
}
