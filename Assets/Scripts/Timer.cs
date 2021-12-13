using UnityEngine;


namespace AirRace
{
    public class Timer : MonoBehaviour
    {
        private float time = 0;

        public void Run(int seconds)
        {
            if (seconds < 0)
            {
                seconds = 0;
            }
            time = seconds;
        }

        private void Update()
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
                Debug.Log("Timer: " + RemaingSeconds);
            }
        }

        public int RemaingSeconds { get => Mathf.CeilToInt(time); }
        public bool IsFinished { get => time <= 0; }

    }
}
