using System.Collections.Generic;
using UnityEngine;

namespace AirRace.Core.SOs
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Leaderboard")]
    public class LeaderboardSO : ScriptableObject
    {
        [SerializeField]
        private int LEADERBOARD_SIZE = 10;

        [SerializeField]
        private List<float> _leaderboard = new List<float>();

        public void AddEntry(float time)
        {
            _leaderboard.Add(time);

            _leaderboard.Sort(); //default is ascending order

            // Trims to SIZE
            if (_leaderboard.Count > LEADERBOARD_SIZE)
            {
                _leaderboard = _leaderboard.GetRange(0, LEADERBOARD_SIZE);
            }
        }

        public List<float> Values()
        {
            return new List<float>(_leaderboard);
        }

        public int Size()
        {
            return LEADERBOARD_SIZE;
        }

        public LeaderboardSerializable ToSerializable()
        {
            return new LeaderboardSerializable(LEADERBOARD_SIZE, _leaderboard);
        }
    }
}