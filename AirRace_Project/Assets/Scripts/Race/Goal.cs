using AirRace.Core;
using UnityEngine;
using UnityEngine.Events;

namespace AirRace.Race
{
    public class Goal : MonoBehaviour
    {
        [SerializeField] private UnityEvent GoalHit;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("GoalHitter"))
            {
                GameLogger.Debug(name + " raised GoalHit");
                GoalHit.Invoke();
            }
        }
    }
}