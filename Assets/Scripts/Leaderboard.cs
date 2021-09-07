using System.Collections.Generic;

namespace AirRace
{
    public class Leaderboard
    {
        public static readonly int SIZE = 10;

        private List<float> _times;
        public List<float> Times { get => new List<float>(_times); }

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