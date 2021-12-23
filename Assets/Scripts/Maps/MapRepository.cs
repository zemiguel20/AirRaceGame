using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace AirRace
{
    public class MapRepository : MonoBehaviour
    {
        [SerializeField] private Map[] _maps;

        public Map[] maps { get { return _maps; } }

        private void Awake()
        {
            //Load leaderboards from files
            foreach (Map map in maps)
            {
                map.leaderboard.SetTimes(LoadLeaderboard(map.name).times);
            }
        }

        private void OnDestroy()
        {
            //Save leaderboards to files
            foreach (Map map in maps)
            {
                SaveLeaderboard(map.leaderboard, map.name);
            }
        }

        public void SaveLeaderboard(Leaderboard leaderboard, string mapName)
        {
            string filepath = Application.persistentDataPath + "/" + mapName + ".leaderboard";
            Debug.Log(JsonUtility.ToJson(leaderboard));
            File.WriteAllText(filepath, JsonUtility.ToJson(leaderboard));
        }

        public Leaderboard LoadLeaderboard(string mapName)
        {
            string filepath = Application.persistentDataPath + "/" + mapName + ".leaderboard";

            if (File.Exists(filepath))
            {
                return JsonUtility.FromJson<Leaderboard>(File.ReadAllText(filepath));
            }
            else
            {
                return new Leaderboard();
            }
        }
    }
}
