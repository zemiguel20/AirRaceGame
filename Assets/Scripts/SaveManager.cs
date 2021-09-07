using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using AirRace.Utils;
using System.Collections.Generic;

namespace AirRace
{
    public static class SaveManager
    {
        private static readonly string SavePath = Application.persistentDataPath + "/";

        public static void SaveLeaderboard(List<float> values, string mapName)
        {
            string filepath = SavePath + mapName + ".save";
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(filepath, FileMode.Create);
            formatter.Serialize(stream, values);
            stream.Close();
        }

        public static List<float> LoadLeaderboard(string mapName)
        {
            string filepath = SavePath + mapName + ".save";

            if (File.Exists(filepath))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(filepath, FileMode.Open);
                List<float> values = formatter.Deserialize(stream) as List<float>;
                stream.Close();
                return values;
            }
            else
            {
                GameLogger.Debug("File not found: " + filepath);
                return null;
            }
        }

    }
}