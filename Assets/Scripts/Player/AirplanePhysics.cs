using System;
using AirRace.Core;
using UnityEngine;

namespace AirRace.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class AirplanePhysics : MonoBehaviour
    {

        public Rigidbody Plane { get; private set; }

        [NonSerialized] public float RollInputMultiplier;
        [NonSerialized] public float PitchInputMultiplier;
        [NonSerialized] public float YawInputMultiplier;
        [NonSerialized] public float ThrustInputMultiplier;

        [SerializeField] [Tooltip("Acceleration in m/s^2")] [Min(0)] private float _thrustAcceleration;

        [SerializeField] [Tooltip("Force multiplier on Roll axis")] [Min(0)] private float _rollForceMultiplier;
        [SerializeField] [Tooltip("Force multiplier on Pitch axis")] [Min(0)] private float _pitchForceMultiplier;
        [SerializeField] [Tooltip("Force multiplier on Yaw axis")] [Min(0)] private float _yawForceMultiplier;

        [SerializeField] [Tooltip("Angle of attack at which lift is maximum and starts decreasing.")] private float _stallAngle;
        [SerializeField] [Tooltip("Max lift coeficcient when stall angle is reached")] private float _maxLiftCf;

        private float[,] _liftCoefficientTable;

        [SerializeField] [Tooltip("Lowest drag coefficient")] private float _minDragCf;
        [SerializeField] [Tooltip("Max drag coefficient")] private float _maxDragCf;

        [SerializeField] private float _wingArea;

        private float _airDensity = 1.225f;

        private void Awake()
        {
            Plane = GetComponent<Rigidbody>();

            _liftCoefficientTable = new float[6, 2];
            _liftCoefficientTable[0, 0] = -180;
            _liftCoefficientTable[1, 0] = -180 + _stallAngle;
            _liftCoefficientTable[2, 0] = -_stallAngle;
            _liftCoefficientTable[3, 0] = _stallAngle;
            _liftCoefficientTable[4, 0] = 180 - _stallAngle;
            _liftCoefficientTable[5, 0] = 180;

            _liftCoefficientTable[0, 1] = 0;
            _liftCoefficientTable[1, 1] = _maxLiftCf;
            _liftCoefficientTable[2, 1] = -_maxLiftCf;
            _liftCoefficientTable[3, 1] = _maxLiftCf;
            _liftCoefficientTable[4, 1] = -_maxLiftCf;
            _liftCoefficientTable[5, 1] = 0;
        }

        private void FixedUpdate()
        {
            ApplyThrustForce();
            ApplyRotationForces();

            float AoA = CalculateAngleOfAttack();

            ApplyDragForce(AoA);
            ApplyLiftForce(AoA);
        }

        private void ApplyThrustForce()
        {
            float magnitude = _thrustAcceleration * ThrustInputMultiplier;
            Vector3 direction = Vector3.forward;

            Plane.AddRelativeForce(direction * magnitude, ForceMode.Acceleration);
        }

        private void ApplyRotationForces()
        {
            Vector3 rollComponent = _rollForceMultiplier * RollInputMultiplier * Vector3.forward;
            Vector3 pitchComponent = _pitchForceMultiplier * PitchInputMultiplier * Vector3.right;
            Vector3 yawComponent = _yawForceMultiplier * YawInputMultiplier * Vector3.up;
            Vector3 torque = Plane.velocity.magnitude * (rollComponent + pitchComponent + yawComponent);
            Plane.AddRelativeTorque(torque, ForceMode.Force);
        }

        private float CalculateAngleOfAttack()
        {
            Vector3 planeDir = Plane.transform.forward;
            Vector3 vel = Plane.velocity;
            Vector3 axis = Plane.transform.right;
            return Vector3.SignedAngle(planeDir, vel, axis);
        }


        private void ApplyDragForce(float AoA)
        {
            float dragCf = CalculateDragCoefficient(AoA);
            float magnitude = dragCf * _wingArea * 0.5f * _airDensity * Mathf.Pow(Plane.velocity.magnitude, 2);
            Vector3 direction = Plane.velocity.normalized * -1;
            Plane.AddForce(direction * magnitude, ForceMode.Force);
        }

        private float CalculateDragCoefficient(float angleOfAttack)
        {
            return (_maxDragCf - _minDragCf) * Mathf.Pow(Mathf.Sin(angleOfAttack), 2);
        }

        private void ApplyLiftForce(float AoA)
        {
            float liftCf = CalculateLiftCoefficient(AoA);
            float magnitude = liftCf * _wingArea * 0.5f * _airDensity * Mathf.Pow(Plane.velocity.magnitude, 2);
            Vector3 direction = Plane.transform.up;
            Plane.AddForce(direction * magnitude, ForceMode.Force);
        }

        private float CalculateLiftCoefficient(float angleOfAttack)
        {
            float Cf1 = 0;
            float Cf2 = 0.0000000000000000001f;

            for (int i = 1; i < 6; i++)
            {
                float lowerLimAngle = _liftCoefficientTable[i - 1, 0];
                float upperLimAngle = _liftCoefficientTable[i, 0];

                if (angleOfAttack >= lowerLimAngle && angleOfAttack <= upperLimAngle)
                {
                    Cf1 = _liftCoefficientTable[i - 1, 1];
                    Cf2 = _liftCoefficientTable[i, 1];
                    break;
                }
            }

            float t = (angleOfAttack - Cf1) / (Cf2 - Cf1);
            return Mathf.Lerp(Cf1, Cf2, t);
        }
    }
}
