using DarkSoulsOBSOverlay.Models.Mappings;
using PropertyHook;
using System;
using System.Collections.Generic;
using System.Text;

namespace DarkSoulsOBSOverlay.Services.AOB
{
    public class DSRHook : PHook
    {
        private DSROffsets Offsets;

        public PHPointer ChrClassBasePtr;
        public PHPointer ChrClassWarp;
        public PHPointer ChrFollowCam;
        public PHPointer WorldChrBase;
        public PHPointer CountersBase;
        public PHPointer AreaBase;
        public PHPointer ChrData1;
        public PHPointer ChrMapData;
        public PHPointer ChrPosData;
        public PHPointer ChrData2;
        public PHPointer EventFlags;

        public DSRHook(int refreshInterval, int minLifetime) :
            base(refreshInterval, minLifetime, p => p.MainWindowTitle == "DARK SOULS™: REMASTERED")
        {
            Offsets = new DSROffsets();
            ChrFollowCam = RegisterRelativeAOB(DSROffsets.ChrFollowCamAOB, 3, 7, DSROffsets.ChrFollowCamOffset1, DSROffsets.ChrFollowCamOffset2, DSROffsets.ChrFollowCamOffset3);
            ChrClassWarp = RegisterRelativeAOB(DSROffsets.ChrClassWarpAOB, 3, 7, DSROffsets.ChrClassWarpOffset1);
            WorldChrBase = RegisterRelativeAOB(DSROffsets.WorldChrBaseAOB, 3, 7, DSROffsets.WorldChrBaseOffset1);
            ChrClassBasePtr = RegisterRelativeAOB(DSROffsets.ChrClassBaseAOB, 3, 7);
            EventFlags = RegisterRelativeAOB(DSROffsets.EventFlagsAOB, 3, 7, DSROffsets.EventFlagsOffset1, DSROffsets.EventFlagsOffset2);
            AreaBase = RegisterRelativeAOB(DSROffsets.AreaBaseAOB, 3, 7, DSROffsets.AreaOffset1);
            CountersBase = RegisterRelativeAOB(DSROffsets.CountersBaseAOB, 3, 7, DSROffsets.CountersOffset1);

            ChrData1 = CreateChildPointer(WorldChrBase, (int)DSROffsets.WorldChrBase.ChrData1);
            ChrMapData = CreateBasePointer(IntPtr.Zero);
            ChrPosData = CreateBasePointer(IntPtr.Zero);
            ChrData2 = CreateChildPointer(ChrClassBasePtr, DSROffsets.ChrData2Offset1, DSROffsets.ChrData2Offset2);

            OnHooked += DSRHook_OnHooked;
        }

        private void DSRHook_OnHooked(object sender, PHEventArgs e)
        {
            Offsets = DSROffsets.GetOffsets(Process.MainModule.ModuleMemorySize);
            ChrMapData = CreateChildPointer(ChrData1, (int)DSROffsets.ChrData1.ChrMapData + Offsets.ChrData1Boost1);
            ChrPosData = CreateChildPointer(ChrMapData, (int)DSROffsets.ChrMapData.ChrPosData);
        }

        private static readonly Dictionary<int, string> VersionStrings = new Dictionary<int, string>
        {
            [0x4869400] = "1.01",
            [0x496BE00] = "1.01.1",
            [0x37CB400] = "1.01.2",
            [0x3817800] = "1.03",
        };

        public string Version
        {
            get
            {
                if (Hooked)
                {
                    int size = Process.MainModule.ModuleMemorySize;
                    if (VersionStrings.TryGetValue(size, out string version))
                        return version;
                    else
                        return $"0x{size:X8}";
                }
                else
                {
                    return "N/A";
                }
            }
        }

        public bool Loaded => ChrFollowCam.Resolve() != IntPtr.Zero;

        public int Health
        {
            get => ChrData1.ReadInt32((int)DSROffsets.ChrData1.Health + Offsets.ChrData1Boost2);
        }

        public int HealthMax
        {
            get => ChrData1.ReadInt32((int)DSROffsets.ChrData1.MaxHealth + Offsets.ChrData1Boost2);
        }

        public int Stamina
        {
            get => ChrData1.ReadInt32((int)DSROffsets.ChrData1.Stamina + Offsets.ChrData1Boost2);
        }

        public int StaminaMax
        {
            get => ChrData1.ReadInt32((int)DSROffsets.ChrData1.MaxStamina + Offsets.ChrData1Boost2);
        }

        public void GetPosition(out float x, out float y, out float z, out float angle)
        {
            x = ChrPosData.ReadSingle((int)DSROffsets.ChrPosData.PosX);
            y = ChrPosData.ReadSingle((int)DSROffsets.ChrPosData.PosY);
            z = ChrPosData.ReadSingle((int)DSROffsets.ChrPosData.PosZ);
            angle = ChrPosData.ReadSingle((int)DSROffsets.ChrPosData.PosAngle);
        }

        public void GetStablePosition(out float x, out float y, out float z, out float angle)
        {
            x = ChrClassWarp.ReadSingle((int)DSROffsets.ChrClassWarp.StableX + Offsets.ChrClassWarpBoost);
            y = ChrClassWarp.ReadSingle((int)DSROffsets.ChrClassWarp.StableY + Offsets.ChrClassWarpBoost);
            z = ChrClassWarp.ReadSingle((int)DSROffsets.ChrClassWarp.StableZ + Offsets.ChrClassWarpBoost);
            angle = ChrClassWarp.ReadSingle((int)DSROffsets.ChrClassWarp.StableAngle + Offsets.ChrClassWarpBoost);
        }

        public bool DeathCam
        {
            get => WorldChrBase.ReadBoolean((int)DSROffsets.WorldChrBase.DeathCam);
        }

        public int SaveSlot
        {
            get => ChrClassWarp.ReadInt32((int)DSROffsets.ChrClassWarp.SaveSlot);
        }

        public int LastBonfire
        {
            get => ChrClassWarp.ReadInt32((int)DSROffsets.ChrClassWarp.LastBonfire + Offsets.ChrClassWarpBoost);
        }

        public int Deaths
        {
            get => CountersBase.ReadInt32((int)DSROffsets.Counters.Deaths);
        }

        public long Clock
        {
            get => CountersBase.ReadInt64((int)DSROffsets.Counters.Clock);
        }

        public double GetSeconds()
        {
            return Math.Floor((double)(Clock / 1000));
        }

        public string GetLastBonfire()
        {
            if(Mappings.Bonfires.TryGetValue(LastBonfire, out string bonfire))
                return bonfire;
            return null;
        }

        public byte[] DumpFollowCam()
        {
            return ChrFollowCam.ReadBytes(0, 512);
        }

        public string CharName
        {
            get => ChrData2.ReadString((int)DSROffsets.ChrData2.Name, Encoding.Unicode, 26);
        }

        public int Area
        {
            get => AreaBase.ReadInt32((int)DSROffsets.Area.AreaCode);
        }

        public string GetArea()
        {
            if(Mappings.Places.TryGetValue(Area, out string area))
                return area;
            return null;
        }

        public byte Class
        {
            get => ChrData2.ReadByte((int)DSROffsets.ChrData2.Class);
        }

        public string GetClass()
        {
            if(Mappings.Classes.TryGetValue(Convert.ToInt32(Class), out string charClass))
                return charClass;
            return null;
        }

        public int Humanity
        {
            get => ChrData2.ReadInt32((int)DSROffsets.ChrData2.Humanity);
        }

        public int Souls
        {
            get => ChrData2.ReadInt32((int)DSROffsets.ChrData2.Souls);
        }

        public int SoulLevel
        {
            get => ChrData2.ReadInt32((int)DSROffsets.ChrData2.SoulLevel);
        }

        public int Vitality
        {
            get => ChrData2.ReadInt32((int)DSROffsets.ChrData2.Vitality);
        }

        public int Attunement
        {
            get => ChrData2.ReadInt32((int)DSROffsets.ChrData2.Attunement);
        }

        public int Endurance
        {
            get => ChrData2.ReadInt32((int)DSROffsets.ChrData2.Endurance);
        }

        public int Strength
        {
            get => ChrData2.ReadInt32((int)DSROffsets.ChrData2.Strength);
        }

        public int Dexterity
        {
            get => ChrData2.ReadInt32((int)DSROffsets.ChrData2.Dexterity);
        }

        public int Resistance
        {
            get => ChrData2.ReadInt32((int)DSROffsets.ChrData2.Resistance);
        }

        public int Intelligence
        {
            get => ChrData2.ReadInt32((int)DSROffsets.ChrData2.Intelligence);
        }

        public int Faith
        {
            get => ChrData2.ReadInt32((int)DSROffsets.ChrData2.Faith);
        }

        private static Dictionary<string, int> eventFlagGroups = new Dictionary<string, int>()
        {
            {"0", 0x00000},
            {"1", 0x00500},
            {"5", 0x05F00},
            {"6", 0x0B900},
            {"7", 0x11300},
        };

        private static Dictionary<string, int> eventFlagAreas = new Dictionary<string, int>()
        {
            {"000", 00},
            {"100", 01},
            {"101", 02},
            {"102", 03},
            {"110", 04},
            {"120", 05},
            {"121", 06},
            {"130", 07},
            {"131", 08},
            {"132", 09},
            {"140", 10},
            {"141", 11},
            {"150", 12},
            {"151", 13},
            {"160", 14},
            {"170", 15},
            {"180", 16},
            {"181", 17},
        };

        private int getEventFlagOffset(int ID, out uint mask)
        {
            string idString = ID.ToString("D8");
            if (idString.Length == 8)
            {
                string group = idString.Substring(0, 1);
                string area = idString.Substring(1, 3);
                int section = Int32.Parse(idString.Substring(4, 1));
                int number = Int32.Parse(idString.Substring(5, 3));

                if (eventFlagGroups.ContainsKey(group) && eventFlagAreas.ContainsKey(area))
                {
                    int offset = eventFlagGroups[group];
                    offset += eventFlagAreas[area] * 0x500;
                    offset += section * 128;
                    offset += (number - (number % 32)) / 8;

                    mask = 0x80000000 >> (number % 32);
                    return offset;
                }
            }
            throw new ArgumentException("Unknown event flag ID: " + ID);
        }

        public bool ReadEventFlag(int ID)
        {
            int offset = getEventFlagOffset(ID, out uint mask);
            return EventFlags.ReadFlag32(offset, mask);
        }

    }
}
