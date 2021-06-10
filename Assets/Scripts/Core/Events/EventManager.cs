using UnityEngine;

namespace AirRace.Core.Events
{
    public class EventManager : MonoBehaviour
    {
        public delegate void RaceStartHandler();
        public event RaceStartHandler RaceStarted;

        public delegate void RaceEndHandler(float time);
        public event RaceEndHandler RaceEnded;

        public delegate void GoalPassedHandler(GameObject goal);
        public event GoalPassedHandler GoalPassed;


        public void RaiseRaceStartedEvent()
        {
            RaceStarted?.Invoke();
        }

        public void RaiseRaceEndedEvent(float finalTime)
        {
            RaceEnded?.Invoke(finalTime);
        }

        public void RaiseGoalPassedEvent(GameObject goalPassed)
        {
            GoalPassed?.Invoke(goalPassed);
        }
    }
}