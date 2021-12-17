using UnityEngine;
using System.Collections;
using System;

namespace AirRace
{
    public class RaceController : MonoBehaviour
    {
        public static event Action<Timer> countdownStarted;
        public static event Action raceFinished;

        private Timer countdownTimer;
        [SerializeField] private int countdownTimeSeconds;

        private AirplanePhysics airplanePhysics;

        [SerializeField] private Goal[] path;
        private int _goalsPassedCount;

        private Chronometer chronometer;

        private void Awake()
        {
            countdownTimer = GetComponent<Timer>();
            airplanePhysics = FindObjectOfType<AirplanePhysics>();
            chronometer = GetComponent<Chronometer>();

            Goal.passed += OnGoalPassed;
        }

        private void Start()
        {
            //Sets all goals inactive except the first
            foreach (Goal goal in path)
            {
                goal.gameObject.SetActive(false);
            }
            path[0].gameObject.SetActive(true);

            _goalsPassedCount = 0;

            chronometer.ResetTime();

            StartCoroutine(StartRace());
        }

        private void OnGoalPassed(Goal obj)
        {
            //Set passed goal inactive and increment count
            obj.gameObject.SetActive(false);
            _goalsPassedCount++;

            if (_goalsPassedCount == path.Length)
            {
                EndRace();
            }
            else
            {
                //Set next Goal active, goalsPassedCount can be used as index since indexing starts at 0
                path[_goalsPassedCount].gameObject.SetActive(true);
            }
        }

        private IEnumerator StartRace()
        {
            airplanePhysics.SetEnabled(false);

            //Start the Countdown
            countdownTimer.Run(countdownTimeSeconds);
            countdownStarted?.Invoke(countdownTimer);
            //Yield execution until Countdown finished
            yield return new WaitUntil(() => countdownTimer.IsFinished);

            airplanePhysics.SetEnabled(true);
            chronometer.StartCounting();
        }

        private void EndRace()
        {
            chronometer.StopCounting();
            airplanePhysics.SetEnabled(false);

            raceFinished?.Invoke();
        }

        public int pathLength { get => path.Length; }
        public int goalsPassedCount { get => _goalsPassedCount; }
        public Goal currentGoal
        {
            get
            {
                //goalsPassedCount can be used as index since indexing starts at 0
                //If all goals passed, return the last one
                if (_goalsPassedCount < path.Length)
                    return path[_goalsPassedCount];
                else
                    return path[path.Length - 1];
            }
        }

        //Clean up when object is destroyed
        private void OnDestroy()
        {
            //Unsubscribe from events
            Goal.passed -= OnGoalPassed;
        }
    }
}