using System.Collections.Generic;

namespace AirRace
{
    [System.Serializable]
    public class Leaderboard
    {
        public static readonly int SIZE = 10;
        private List<float> _times;
        public float[] Times { get => _times.ToArray(); }

        public Leaderboard()
        {
            _times = new List<float>();
        }

        public void AddEntry(float time)
        {
            _times.Add(time);
            SortAndTrim();
        }

        public void SetTimes(float[] times)
        {
            _times = new List<float>(times);
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