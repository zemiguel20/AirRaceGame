using System.Collections.Generic;

namespace AirRace.Leaderboard
{
    [System.Serializable]
    public class LeaderboardSerializable
    {
        public int size;
        public float[] times;

        public LeaderboardSerializable(int size, List<float> times)
        {
            this.size = size;
            this.times = times.ToArray();
        }
    }
}