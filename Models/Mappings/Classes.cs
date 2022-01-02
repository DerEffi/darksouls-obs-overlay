using System.Collections.Generic;

namespace DarkSoulsOBSOverlay.Models.Mappings
{
    public static partial class Mappings
    {
        public static readonly Dictionary<int, string> Classes = new()
        {
            { 0, "Warrior" },
            { 1, "Knight" },
            { 2, "Wanderer" },
            { 3, "Thief" },
            { 4, "Bandit" },
            { 5, "Hunter" },
            { 6, "Sorcerer" },
            { 7, "Pyromancer" },
            { 8, "Cleric" },
            { 9, "Deprived" },
        };
    }
}
