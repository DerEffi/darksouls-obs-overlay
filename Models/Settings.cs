namespace DarkSoulsOBSOverlay.Models
{
    public class Settings
    {
        public int UpdateInterval { get; set; } = 1;
        public bool AreaBasedEventQuery { get; set; } = false;
        public bool HealthEnabled { get; set; } = false;
        public bool ClockEnabled { get; set; } = true;
    }
}
