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

        public static void SaveLeaderboard(LeaderboardSerializable leaderboard, string filename)
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

        public static List<MapInfoSO> LoadAllMapInfos()
        {
            var maps = Resources.LoadAll<MapInfoSO>("MapInfo");
            foreach (var mapInfo in maps)
            {
                string scriptableObjectName = mapInfo.name;
                Leaderboard leaderboard = LoadLeaderboard(scriptableObjectName);
                if (leaderboard != null)
                {
                    mapInfo.Leaderboard.SetTimes(leaderboard.Times);
                }
            }

            return new List<MapInfoSO>(maps);
        }

    }
}