using System;
using System.Collections.Generic;

namespace DarkSoulsOBSOverlay.Services.AOB
{
    public class DSROffsets
    {
        public const string CamManBaseAOB = "48 8B 05 ? ? ? ? 48 63 D1 48 8B 44 D0 08 C3";
        public const int CamManOffset = 0x10;

        public const string ChrFollowCamAOB = "48 8B 0D ? ? ? ? E8 ? ? ? ? 48 8B 4E 68 48 8B 05 ? ? ? ? 48 89 48 60";
        public const int ChrFollowCamOffset1 = 0;
        public const int ChrFollowCamOffset2 = 0x60;
        public const int ChrFollowCamOffset3 = 0x60;

        public const string ChrClassWarpAOB = "48 8B 05 ? ? ? ? 66 0F 7F 80 ? ? ? ? 0F 28 02 66 0F 7F 80 ? ? ? ? C6 80";
        public const int ChrClassWarpOffset1 = 0;
        public int ChrClassWarpBoost = 0x0;
        public enum ChrClassWarp
        {
            SaveSlot = 0xAA0,
            LastBonfire = 0xB24,
            StableX = 0xB90,
            StableY = 0xB94,
            StableZ = 0xB98,
            StableAngle = 0xBA4,
        }

        public const string WorldChrBaseAOB = "48 8B 05 ? ? ? ? 48 8B 48 68 48 85 C9 0F 84 ? ? ? ? 48 39 5E 10 0F 84 ? ? ? ? 48";
        public const int WorldChrBaseOffset1 = 0;
        public enum WorldChrBase
        {
            ChrData1 = 0x68,
            DeathCam = 0x70,
        }

        public int ChrData1Boost1 = 0x0;
        public int ChrData1Boost2 = 0x0;
        public enum ChrData1
        {
            ChrMapData = 0x48,
            ChrFlags1 = 0x284,
            Health = 0x3D8,
            MaxHealth = 0x3DC,
            Stamina = 0x3E8,
            MaxStamina = 0x3EC,
            ChrFlags2 = 0x514,
        }

        public enum ChrMapData
        {
            ChrAnimData = 0x18,
            ChrPosData = 0x28,
            ChrMapFlags = 0x104,
            Warp = 0x108,
            WarpX = 0x110,
            WarpY = 0x114,
            WarpZ = 0x118,
            WarpAngle = 0x124,
        }

        public enum ChrPosData
        {
            PosAngle = 0x4,
            PosX = 0x10,
            PosY = 0x14,
            PosZ = 0x18,
        }

        public const string ChrClassBaseAOB = "48 8B 05 ? ? ? ? 48 85 C0 ? ? F3 0F 58 80 AC 00 00 00";
        public const int ChrData2Offset1 = 0;
        public const int ChrData2Offset2 = 0x10;
        public enum ChrData2
        {
            Vitality = 0x40,
            Attunement = 0x48,
            Endurance = 0x50,
            Strength = 0x58,
            Dexterity = 0x60,
            Intelligence = 0x68,
            Faith = 0x70,
            Humanity = 0x84,
            Resistance = 0x88,
            SoulLevel = 0x90,
            Souls = 0x94,
            Gender = 0xCA,
            Class = 0xCE,
            Name = 0xA8
        }

        public const string AreaBaseAOB = "48 8B 05 ? ? ? ? 48 C7 80 74 0F 00 00 FF FF FF FF C7 80 7C 0F 00 00 FF FF FF FF";
        public const int AreaOffset1 = 0x0;
        public enum Area
        {
            AreaCode = 0xF74,
        }

        public const string CountersBaseAOB = "48 8B 05 ? ? ? ? 81 B8 A4 00 00 00 ? ? ? ? ? ? C7 80 A4 00 00 00 ? ? ? ?"; //Code that accesses clock
        public const int CountersOffset1 = 0x0;
        public enum Counters
        {
            Deaths = 0x98,
            Clock = 0xA4,
        }

        public const string EventFlagsAOB = "48 8B 0D ? ? ? ? 99 33 C2 45 33 C0 2B C2 8D 50 F6";
        public const int EventFlagsOffset1 = 0;
        public const int EventFlagsOffset2 = 0;

        public const string ItemGetAOB = "48 89 5C 24 18 89 54 24 10 55 56 57 41 54 41 55 41 56 41 57 48 8D 6C 24 F9";

        public const string BonfireWarpAOB = "48 89 5C 24 08 57 48 83 EC 20 48 8B D9 8B FA 48 8B 49 08 48 85 C9 0F 84 ? ? ? ? E8 ? ? ? ? 48 8B 4B 08";

        public static DSROffsets GetOffsets(int moduleSize)
        {
            DSROffsets result = new();
            int version = versions.ContainsKey(moduleSize) ? versions[moduleSize] : 100;

            if (version > 1)
            {
                result.ChrClassWarpBoost = 0x10;
            }

            if (version > 2)
            {
                result.ChrData1Boost1 = 0x20;
                result.ChrData1Boost2 = 0x10;
            }

            return result;
        }

        private static readonly Dictionary<int, int> versions = new Dictionary<int, int>()
        {
            [0x4869400] = 0, // 1.01
            [0x496BE00] = 1, // 1.01.1
            [0x37CB400] = 2, // 1.01.2
            [0x3817800] = 3, // 1.03
        };
    }
}
