using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AirRace.Core
{
    public static class SaveManager
    {
        private static string SavePath = Application.persistentDataPath + "/";

        public static void SaveLeaderboard(LeaderboardSerializable leaderboard, string filename)
        {
            string filepath = SavePath + filename + ".save";
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(filepath, FileMode.Create);
            formatter.Serialize(stream, leaderboard);
            stream.Close();
        }

        public static LeaderboardSerializable LoadLeaderboard(string filename)
        {
            string filepath = SavePath + filename + ".save";

            if (File.Exists(filepath))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(filepath, FileMode.Open);
                LeaderboardSerializable leaderboard = formatter.Deserialize(stream) as LeaderboardSerializable;
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