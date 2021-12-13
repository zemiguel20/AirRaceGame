using UnityEngine;
using System.Collections;

namespace AirRace.Race
{
    public class RaceController : MonoBehaviour
    {
        private Timer countdownTimer;

        [SerializeField] private int countdownTimeSeconds;

        private void Awake()
        {
            countdownTimer = GetComponent<Timer>();
        }

        private void Start()
        {
            StartCoroutine(StartCountdown());
        }

        private IEnumerator StartCountdown()
        {
            Debug.Log("Timer Started");

            //Start the Timer
            countdownTimer.Run(countdownTimeSeconds);

            //Yield execution until timer finished
            yield return new WaitUntil(() => countdownTimer.IsFinished);

            Debug.Log("Timer Finished");
        }
    }
}