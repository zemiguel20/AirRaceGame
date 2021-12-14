using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

namespace AirRace
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private UIDocument hud;
        private Label chronometerLabel;
        private Label goalCountLabel;
        private Label timerLabel;

        private RaceController raceController;
        private Chronometer chronometer;


        private Transform airplane;
        private Camera playerCamera;
        [SerializeField] private GameObject waypoint;

        private void Awake()
        {
            raceController = FindObjectOfType<RaceController>();
            chronometer = FindObjectOfType<Chronometer>();
            airplane = GameObject.Find("Airplane").GetComponent<Transform>();
            playerCamera = Camera.main;

            chronometerLabel = hud.rootVisualElement.Query<Label>("chronometer");
            goalCountLabel = hud.rootVisualElement.Query<Label>("goal-count");
            timerLabel = hud.rootVisualElement.Query<Label>("timer");

            RaceController.countdownStarted += OnCountdownStarted;
        }

        private void Update()
        {
            chronometerLabel.text = chronometer.time.ToString("F2");
            goalCountLabel.text = raceController.goalsPassedCount + "/" + raceController.pathLength;

            UpdateWaypoint();
        }

        private void UpdateWaypoint()
        {
            Vector3 goalWorldPosition = raceController.currentGoal.transform.position;
            Vector3 playerWorldPosition = airplane.position;

            Vector3 goalPositionRelativeToCamera = playerCamera.transform.InverseTransformPoint(goalWorldPosition);
            Vector3 playerPositionRelativeToCamera = playerCamera.transform.InverseTransformPoint(playerWorldPosition);

            /*
             * Waypoint "mirrors" Camera position.
             * Target position relative to Waypoint.
             */
            Vector3 target = goalPositionRelativeToCamera - playerPositionRelativeToCamera + waypoint.transform.position;
            waypoint.transform.LookAt(target);
        }

        private void OnCountdownStarted(Timer timer)
        {
            StartCoroutine(TimerSequence(timer));
        }

        private IEnumerator TimerSequence(Timer timer)
        {
            while (timer.IsFinished == false)
            {
                timerLabel.text = timer.RemaingSeconds.ToString();
                yield return null;
            }

            //Show GO for a short perior and then turn invisible
            timerLabel.text = "GO!";
            yield return new WaitForSeconds(1);
            timerLabel.visible = false;
        }

        //Cleanup on object destroy
        private void OnDestroy()
        {
            //Unsub from events
            RaceController.countdownStarted -= OnCountdownStarted;
        }
    }
}
