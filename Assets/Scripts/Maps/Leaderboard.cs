using System.Collections.Generic;
using System;
using UnityEngine;

namespace AirRace
{
    [Serializable]
    public class Leaderboard
    {
        public const int SIZE = 10;

        [SerializeField] private List<float> _times;
        public List<float> times { get => new List<float>(_times); }

        public Leaderboard()
        {
            _times = new List<float>();
        }

        public void AddEntry(float time)
        {
            _times.Add(time);
            SortAndTrim();
        }

        public void SetTimes(List<float> times)
        {
            _times = times;
            SortAndTrim();
        }

        private void SortAndTrim()
        {
            _times.Sort(); //default is ascending order

            // Trims to SIZE
            if (_times.Count > SIZE)
            {
                _times = _times.GetRange(0, SIZE);
            }
        }
    }
}