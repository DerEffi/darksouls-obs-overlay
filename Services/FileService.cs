using DarkSoulsOBSOverlay.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace DarkSoulsOBSOverlay.Services
{
    public class FileService
    {
        private static readonly string Folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/DerEffi/" + Assembly.GetExecutingAssembly().GetName().Name + "/";
        private static readonly string LogFile = Folder + "Logs.csv";
        private static readonly string SettingsFile = Folder + "Settings.json";
        private static readonly string SaveDataFile = Folder + "SaveData.json";

        public static Settings LoadSettings()
        {
            try
            {
                if (!File.Exists(SettingsFile))
                {
                    return new();
                }

                return JsonConvert.DeserializeObject<Settings>(File.ReadAllText(SettingsFile));
            } catch
            {
                return new();
            }
        }

        public static void SaveSettings(Settings settings)
        {
            try
            {
                File.WriteAllText(SettingsFile, JsonConvert.SerializeObject(settings));
            } catch {}
        }

        public static void WriteEventLog(DarkSoulsData data)
        {
            try
            {
                if(!File.Exists(LogFile))
                {
                    File.WriteAllText(LogFile, "Clock;State;Flag;Value");
                }

                data.UpdatedEventFlags.ForEach(flag =>
                {
                    File.AppendAllText(LogFile, $"{data.Clock};{(data.Loaded ? "InGame" : "Loading")};{flag.Key};{flag.Value}");
                });
            } catch { }
        }
    }
}
