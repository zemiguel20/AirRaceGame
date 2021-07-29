using System.Collections;
using AirRace.Race;
using TMPro;
using UnityEngine;

namespace AirRace.UI
{
    public class CountdownTimerUI : MonoBehaviour
    {
        private RaceController _raceController;
        private TextMeshProUGUI tmpText;

        private bool countdownRunning = false;

        public void Initialize(RaceController controller)
        {
            tmpText = GetComponent<TextMeshProUGUI>();
            tmpText.text = "Starting in...";

            _raceController = controller;
            _raceController.CountdownStarted += OnCountdownStarted;
            _raceController.CountdownFinished += OnCountdownFinished;
        }

        private void OnCountdownStarted()
        {
            countdownRunning = true;
        }

        private void Update()
        {
            if (countdownRunning)
            {
                tmpText.text = _raceController.Timer.RemaingSeconds.ToString();
            }
        }

        private void OnCountdownFinished()
        {
            StartCoroutine(DisplayFinalTextAndDisable());
        }

        private IEnumerator DisplayFinalTextAndDisable()
        {
            countdownRunning = false;
            tmpText.text = "GO";
            yield return new WaitForSeconds(1.5f);

            gameObject.SetActive(false);
        }

    }
}