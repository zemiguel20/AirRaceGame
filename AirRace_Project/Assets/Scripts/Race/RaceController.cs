using AirRace.Core.Events;
using UnityEngine;

namespace AirRace.Race
{
    public class RaceController : MonoBehaviour
    {
        [SerializeField] private EventManager _eventManager;
        [SerializeField] private Chronometer _chronometer;
        [SerializeField] private PathManager _pathManager;

        private void Start()
        {
            //Subscribe to Events
            _eventManager.RaceStarted += StartRace;
            _eventManager.GoalPassed += OnGoalPassed;
        }

        private void OnDisable()
        {
            // UNsubscribe to Events
            _eventManager.RaceStarted -= StartRace;
            _eventManager.GoalPassed -= OnGoalPassed;
        }

        public void StartRace()
        {
            _chronometer.StartChrono();
            _pathManager.StartPath();

            if (_pathManager.IsFinished())
            {
                EndRace();
            }
        }

        public void OnGoalPassed(GameObject goal)
        {
            _pathManager.ChangeActiveGoal();

            if (_pathManager.IsFinished())
            {
                EndRace();
            }
        }

        private void EndRace()
        {
            _chronometer.StopChrono();
            _eventManager.RaiseRaceEndedEvent(_chronometer.time);
        }

    }
}