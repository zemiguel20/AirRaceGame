using UnityEngine;

namespace AirRace
{
    [CreateAssetMenu(fileName = "MapInfoSO", menuName = "ScriptableObjects/MapInfoSO")]
    public class MapInfoSO : ScriptableObject
    {
        [SerializeField] private string _mapName;
        public string MapName { get => _mapName; }

        [SerializeField] private Sprite _image;
        public Sprite Image { get => _image; }

        private Leaderboard _leaderboard = new Leaderboard();
        public Leaderboard Leaderboard { get => _leaderboard; }

        [SerializeField] private string _sceneName;
        public string SceneName { get => _sceneName; }
    }
}