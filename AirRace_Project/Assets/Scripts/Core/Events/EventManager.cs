using UnityEngine;

namespace AirRace.Core.Events
{
    public class EventManager
    {

        public delegate void RaceStartHandler();
        public static event RaceStartHandler RaceStarted;

        public delegate void RaceEndHandler(int time);
        public static event RaceEndHandler RaceEnded;

        public delegate void GoalPassedHandler(GameObject goal);
        public static event GoalPassedHandler GoalPassed;

    }
}