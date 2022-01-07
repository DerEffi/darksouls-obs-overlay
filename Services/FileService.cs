using DarkSoulsOBSOverlay.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DarkSoulsOBSOverlay.Services
{
    public static class FileService
    {
        private static readonly string Folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\DerEffi\\" + Assembly.GetExecutingAssembly().GetName().Name + "\\";
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
                if (!Directory.Exists(Folder))
                {
                    Directory.CreateDirectory(Folder);
                }
                File.WriteAllText(SettingsFile, JsonConvert.SerializeObject(settings));
            } catch {}
        }

        public static void WriteEventLog(DarkSoulsData data)
        {
            try
            {
                if(!File.Exists(LogFile))
                {
                    if(!Directory.Exists(Folder))
                    {
                        Directory.CreateDirectory(Folder);
                    }
                    File.WriteAllText(LogFile, "Clock;State;Flag;Value");
                }

                data.UpdatedEventFlags.ForEach(flag =>
                {
                    File.AppendAllText(LogFile, $"{data.Char.Clock};{(data.Loaded ? "InGame" : "Loading")};{flag.Key};{flag.Value}");
                });
            } catch { }
        }

        public static DarkSoulsResettedData LoadResettedData(DarkSoulsData data)
        {
            DarkSoulsResettedData saveData = LoadAllResettedData().FirstOrDefault(save => save.CharacterName == data.Char.CharacterName && save.SaveSlot == data.Char.SaveSlot);
            if(saveData != null && (data.Char.Clock - saveData.Clock) >= 0)
            {
                DarkSoulsResettedData newSaveGame = new()
                {
                    CharacterName = data.Char.CharacterName,
                    Clock = data.Char.Clock,
                    SaveSlot = data.Char.SaveSlot,
                    EventFlags = new()
                };
                SaveResettedData(newSaveGame);
                return newSaveGame;
            }
            return saveData;
        }

        private static List<DarkSoulsResettedData> LoadAllResettedData()
        {
            if(!File.Exists(SaveDataFile))
            {
                return new();
            }

            return JsonConvert.DeserializeObject<List<DarkSoulsResettedData>>(File.ReadAllText(SaveDataFile));
        }

        public static void SaveResettedData(DarkSoulsResettedData data)
        {
            bool found = false;
            List<DarkSoulsResettedData> saveGames = LoadAllResettedData();
            saveGames = saveGames.Select(saveGame =>
            {
                if (saveGame.CharacterName == data.CharacterName && saveGame.SaveSlot == data.SaveSlot)
                {
                    found = true;
                    return data;
                }
                return saveGame;
            }).ToList();

            if(!found)
            {
                saveGames.Add(data);
            }

            if (!Directory.Exists(Folder))
            {
                Directory.CreateDirectory(Folder);
            }

            File.WriteAllText(SaveDataFile, JsonConvert.SerializeObject(saveGames));
        }
    }
}
