using AirRace.Core;
using AirRace.Core.GameEvent;
using UnityEngine;

namespace AirRace.Race
{
    public class Goal : MonoBehaviour
    {
        [SerializeField] private GameEvent GoalHit;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("GoalHitter"))
            {
                GameLogger.Debug(name + " raised GoalHit");
                GoalHit.Raise();
            }
        }
    }
}