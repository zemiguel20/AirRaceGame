using System;

namespace AirRace.Utils
{
    public class Chronometer
    {
        private float _timeCount;

        public Chronometer()
        {
            _timeCount = 0;
        }

        public float Time { get => _timeCount; }


        /// <summary> Note: added time is equal to the *absolute* value of given time. </summary>
        public void Tick(float time)
        {
            _timeCount += Math.Abs(time);
        }

    }
}