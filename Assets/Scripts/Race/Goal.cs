using AirRace.Core;
using UnityEngine;

namespace AirRace.Race
{
    public class Goal : MonoBehaviour
    {
        public delegate void GoalPassedHandler(Goal goal);
        public event GoalPassedHandler GoalPassed;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("GoalHitter"))
            {
                GameLogger.Debug(name + " raised GoalHit");
                GoalPassed?.Invoke(this);
            }
        }
    }
}