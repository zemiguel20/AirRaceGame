using AirRace.Core.Events;
using UnityEngine;

namespace AirRace.Player
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private Rigidbody _plane;
        [SerializeField] private EventManager _eventManager;

        [Tooltip("Value for max acceleration. Acceleration is given by Input * MaxAcceleration")]
        [SerializeField] private int MAX_ACCELERATION;

        [Tooltip("Base rotation speed strength")]
        [SerializeField] private float ROTATION_SPEED;

        [Tooltip("Velocity value of the plane at which rotation speed is MAX")]
        [SerializeField] private float ROTATION_SPEED_VELOCITY_THRESHOLD;

        [Tooltip("Value of the velocity of the plane at which the lift is max")]
        [SerializeField] private float LIFT_VELOCITY_THRESHOLD;

        private float inputAcceleretion;
        private float inputAilerons;
        private float inputElevators;

        private void Start()
        {
            inputAcceleretion = 0;
            inputAilerons = 0;
            inputElevators = 0;

            _eventManager.RaceStarted += OnRaceStart;
            _eventManager.RaceEnded += OnRaceEnd;
        }

        private void OnDisable()
        {
            _eventManager.RaceStarted -= OnRaceStart;
            _eventManager.RaceEnded -= OnRaceEnd;
        }

        public void OnRaceStart()
        {
            _plane.isKinematic = false;
        }

        public void OnRaceEnd(float time)
        {
            _plane.isKinematic = true;
        }

        // MOVEMENT ----------------------------------------------------------------------------

        private void FixedUpdate()
        {
            RotatePlane();
            UpdateThrust();
            UpdateLift();
        }

        private void RotatePlane()
        {

            Vector3 direction = new Vector3(inputElevators, 0, -inputAilerons);

            float velocityFactor = Mathf.Clamp01(_plane.velocity.magnitude / ROTATION_SPEED_VELOCITY_THRESHOLD);

            Vector3 force = direction * velocityFactor * ROTATION_SPEED;

            _plane.AddRelativeTorque(force, ForceMode.Acceleration);
        }

        private void UpdateThrust()
        {
            Vector3 force = Vector3.forward * (inputAcceleretion * MAX_ACCELERATION);

            _plane.AddRelativeForce(force, ForceMode.Acceleration);
        }

        private void UpdateLift()
        {
            Vector3 baseForce = Physics.gravity * -1;

            float velocityFactor = Mathf.Clamp01(_plane.velocity.magnitude / LIFT_VELOCITY_THRESHOLD);

            float inclinationFactor = CalculateInclinationFactor();

            Vector3 force = baseForce * velocityFactor * inclinationFactor;

            _plane.AddForce(force, ForceMode.Acceleration);
        }

        private float CalculateInclinationFactor()
        {
            bool isRotating = inputAilerons != 0;

            if (isRotating)
                return 1;

            float angle = _plane.transform.localEulerAngles.z;
            // y = 0.0000205761 * (x-180)^2 + 0.333333
            float factor = 0.0000205761f * Mathf.Pow(angle - 180, 2) + 0.333333f;

            return factor;
        }

        public void OnAccelerate(float value)
        {
            inputAcceleretion = value;
        }

        public void OnAileronsMove(float value)
        {
            inputAilerons = value;
        }

        public void OnElevatorsMove(float value)
        {
            inputElevators = value;
        }

    }
}