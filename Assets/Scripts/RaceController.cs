using UnityEngine;
using System.Collections;

namespace AirRace.Race
{
    public class RaceController : MonoBehaviour
    {
        private Timer countdownTimer;
        [SerializeField] private int countdownTimeSeconds;

        private AirplanePhysics airplanePhysics;

        private void Awake()
        {
            countdownTimer = GetComponent<Timer>();
            airplanePhysics = FindObjectOfType<AirplanePhysics>();
        }

        private void Start()
        {
            StartCoroutine(StartCountdown());
        }

        private IEnumerator StartCountdown()
        {
            airplanePhysics.SetEnabled(false);

            Debug.Log("Timer Started");

            //Start the Timer
            countdownTimer.Run(countdownTimeSeconds);

            //Yield execution until timer finished
            yield return new WaitUntil(() => countdownTimer.IsFinished);

            Debug.Log("Timer Finished");

            airplanePhysics.SetEnabled(true);
        }
    }
}