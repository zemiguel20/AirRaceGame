using UnityEngine;
using TMPro;
using AirRace.Race;
using System.Collections;
using System;

namespace AirRace.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _velocityLabel;
        [SerializeField] private TextMeshProUGUI _altitudeLabel;
        [SerializeField] private TextMeshProUGUI _chronometerLabel;
        [SerializeField] private TextMeshProUGUI _throttleLabel;
        [SerializeField] private TextMeshProUGUI _countdownTimerLabel;
        [SerializeField] private GameObject _waypoint;

        private RaceController _raceController;

        private Camera _playerCamera;

        private bool countdownRunning = false;


        public void Initialize(RaceController raceController)
        {
            _playerCamera = Camera.main;
            _raceController = raceController;
            _raceController.CountdownStarted += OnCountdownStarted;
            _raceController.CountdownFinished += OnCountdownFinished;
        }

        private void OnCountdownStarted()
        {
            countdownRunning = true;
        }

        private void OnCountdownFinished()
        {
            StartCoroutine(DisplayFinalTextAndDisable());
        }

        private IEnumerator DisplayFinalTextAndDisable()
        {
            countdownRunning = false;
            _countdownTimerLabel.text = "GO";
            yield return new WaitForSeconds(1.5f);

            _countdownTimerLabel.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (countdownRunning)
            {
                _countdownTimerLabel.text = _raceController.Timer.RemaingSeconds.ToString();
            }

            _chronometerLabel.text = "CHRONO: " + _raceController.Chronometer.Time.ToString("F2");

            _altitudeLabel.text = "ALT: " + _raceController.Airplane.transform.position.y.ToString("F0") + " m";

            int kmPerH = Mathf.RoundToInt(_raceController.Airplane.Velocity.magnitude * 3.6f);
            _velocityLabel.text = "SPD: " + kmPerH + " km/h";

            int throttlePercent = Mathf.RoundToInt(_raceController.Airplane.Throttle * 100);
            _throttleLabel.text = "THR: " + throttlePercent + "%";

            UpdateWaypoint();

        }

        private void UpdateWaypoint()
        {
            Vector3 goalWorldPosition = _raceController.Path.GetCurrentGoal().transform.position;
            Vector3 playerWorldPosition = _raceController.Airplane.transform.position;

            Vector3 goalLocalPositionRelativeToCamera = _playerCamera.transform.InverseTransformPoint(goalWorldPosition);
            Vector3 playerLocalPositionRelativeToCamera = _playerCamera.transform.InverseTransformPoint(playerWorldPosition);

            /*
             * Waypoint "mirrors" Camera position.
             * Target position relative to Waypoint.
             */
            Vector3 target = goalLocalPositionRelativeToCamera - playerLocalPositionRelativeToCamera + _waypoint.transform.position;
            _waypoint.transform.LookAt(target);
        }


    }
}
