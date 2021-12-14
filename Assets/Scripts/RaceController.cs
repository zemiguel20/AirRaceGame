using UnityEngine;
using System.Collections;

namespace AirRace
{
    public class RaceController : MonoBehaviour
    {
        private Timer countdownTimer;
        [SerializeField] private int countdownTimeSeconds;

        private AirplanePhysics airplanePhysics;

        [SerializeField] private Goal[] path;
        private int goalsPassedCount;

        private Chronometer chronometer;

        private void Awake()
        {
            countdownTimer = GetComponent<Timer>();
            airplanePhysics = FindObjectOfType<AirplanePhysics>();
            chronometer = GetComponent<Chronometer>();
        }

        private void Start()
        {
            //Sets all goals inactive except the first
            foreach (Goal goal in path)
            {
                goal.gameObject.SetActive(false);
            }
            path[0].gameObject.SetActive(true);

            goalsPassedCount = 0;
            Goal.passed += OnGoalPassed;

            chronometer.ResetTime();

            StartCoroutine(StartRace());
        }

        private void OnGoalPassed(Goal obj)
        {
            //Set passed goal inactive and increment count
            obj.gameObject.SetActive(false);
            goalsPassedCount++;

            if (goalsPassedCount == path.Length)
            {
                EndRace();
            }
            else
            {
                //Set next Goal active, goalsPassedCount can be used as index since indexing starts at 0
                path[goalsPassedCount].gameObject.SetActive(true);
            }
        }

        private IEnumerator StartRace()
        {
            airplanePhysics.SetEnabled(false);

            //Start the Countdown
            countdownTimer.Run(countdownTimeSeconds);
            //Yield execution until Countdown finished
            yield return new WaitUntil(() => countdownTimer.IsFinished);

            airplanePhysics.SetEnabled(true);
            chronometer.StartCounting();
        }

        private void EndRace()
        {
            chronometer.StopCounting();
            airplanePhysics.SetEnabled(false);

            Debug.Log(chronometer.time);
        }

        //Clean up when object is destroyed
        private void OnDestroy()
        {
            //Unsubscribe from events
            Goal.passed -= OnGoalPassed;
        }
    }
}