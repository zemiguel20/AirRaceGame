using AirRace.Utils;
using UnityEngine;

namespace AirRace.Race
{
    public class Chronometer : MonoBehaviour
    {
        public float time { get; private set; }

        private bool active = false;

        private void Start()
        {
            time = 0;
        }

        void Update()
        {
            if (active)
            {
                time += Time.deltaTime;
            }
        }

        public void StartChrono()
        {
            GameLogger.Debug("Chronometer started");
            active = true;
        }

        public void StopChrono()
        {
            GameLogger.Debug("Chronometer stopped");
            active = false;
        }

    }
}