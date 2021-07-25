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

        public static void SaveLeaderboard(Leaderboard leaderboard, string filename)
        {
            string filepath = SavePath + filename + ".save";
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(filepath, FileMode.Create);
            formatter.Serialize(stream, leaderboard);
            stream.Close();
        }

        public static Leaderboard LoadLeaderboard(string filename)
        {
            string filepath = SavePath + filename + ".save";

            if (File.Exists(filepath))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(filepath, FileMode.Open);
                Leaderboard leaderboard = formatter.Deserialize(stream) as Leaderboard;
                stream.Close();
                return leaderboard;
            }
            else
            {
                GameLogger.Debug("File not found: " + filepath);
                return null;
            }
        }

    }
}