using AirRace.Race;
using System.Collections;
using UnityEngine;

namespace AirRace
{
    [System.Serializable]
    public class Map
    {
        [SerializeField] private string _mapName;
        public string Name { get => _mapName; }

        [SerializeField] private Sprite _image;
        public Sprite Image { get => _image; }

        private Leaderboard _leaderboard = new Leaderboard();
        public Leaderboard Leaderboard { get => _leaderboard; }

        [SerializeField] private string _sceneName;
        public string SceneName { get => _sceneName; }

        [SerializeField] private Path _pathPrefab;
        public Path PathPrefab { get => _pathPrefab; }
    }
}