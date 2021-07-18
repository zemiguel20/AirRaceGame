using System.Collections.Generic;

namespace AirRace
{
    [System.Serializable]
    public class Leaderboard
    {
        private static readonly int SIZE = 10;
        private List<float> times;

        public Leaderboard()
        {
            times = new List<float>();
        }

        public void AddEntry(float time)
        {
            times.Add(time);

            times.Sort(); //default is ascending order

            // Trims to SIZE
            if (times.Count > SIZE)
            {
                times = times.GetRange(0, SIZE);
            }
        }

        public List<float> Values()
        {
            return new List<float>(times);
        }
    }
}