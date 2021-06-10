using AirRace.Core;
using AirRace.Core.Events;
using UnityEngine;

namespace AirRace.Race
{
    public class Goal : MonoBehaviour
    {
        [SerializeField] private EventManager _eventManager;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("GoalHitter"))
            {
                GameLogger.Debug(name + " raised GoalHit");
                _eventManager.RaiseGoalPassedEvent(this.gameObject);
            }
        }
    }
}