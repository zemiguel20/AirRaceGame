using UnityEngine;

namespace AirRace
{
    [CreateAssetMenu(fileName = "MapInfoSO", menuName = "ScriptableObjects/MapInfoSO")]
    public class MapInfoSO : ScriptableObject
    {
        [SerializeField] string _mapName;
        public string MapName { get => _mapName; }

        [SerializeField] Sprite _image;
        public Sprite Image { get => _image; }

        Leaderboard _leaderboard = new Leaderboard();
        public Leaderboard Leaderboard { get => _leaderboard; }
    }
}