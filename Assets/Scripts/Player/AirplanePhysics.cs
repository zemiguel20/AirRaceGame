using System;
using AirRace.Core;
using AirRace.Core.SOs;
using UnityEngine;

namespace AirRace.Player
{
    public class AirplanePhysics
    {
        private float[,] _liftCoefficientTable;

        private const float AIR_DENSITY = 1.225f;
        private PlanePropertiesSO _planeProperties;
        private Rigidbody _plane;

        public AirplanePhysics(Rigidbody plane, PlanePropertiesSO planeProperties)
        {
            _plane = plane;
            _planeProperties = planeProperties;
            BuildLiftCfTable();
        }

        private void BuildLiftCfTable()
        {
            _liftCoefficientTable = new float[6, 2];
            _liftCoefficientTable[0, 0] = -180;
            _liftCoefficientTable[1, 0] = -180 + _planeProperties.StallAngle;
            _liftCoefficientTable[2, 0] = -_planeProperties.StallAngle;
            _liftCoefficientTable[3, 0] = _planeProperties.StallAngle;
            _liftCoefficientTable[4, 0] = 180 - _planeProperties.StallAngle;
            _liftCoefficientTable[5, 0] = 180;

            _liftCoefficientTable[0, 1] = 0;
            _liftCoefficientTable[1, 1] = _planeProperties.MaxLiftCf;
            _liftCoefficientTable[2, 1] = -_planeProperties.MaxLiftCf;
            _liftCoefficientTable[3, 1] = _planeProperties.MaxLiftCf;
            _liftCoefficientTable[4, 1] = -_planeProperties.MaxLiftCf;
            _liftCoefficientTable[5, 1] = 0;
        }

        public void UpdateForces(float thrustInputMultiplier, float rollInputMultiplier, float pitchInputMultiplier, float yawInputMultiplier)
        {
            ApplyThrustForce(thrustInputMultiplier);
            ApplyRotationForces(rollInputMultiplier, pitchInputMultiplier, yawInputMultiplier);

            float AoA = CalculateAngleOfAttack();

            ApplyDragForce(AoA);
            ApplyLiftForce(AoA);
        }

        private void ApplyThrustForce(float thrustInputMultiplier)
        {
            float magnitude = _planeProperties.MaxAcceleration * thrustInputMultiplier;
            Vector3 direction = Vector3.forward;

            _plane.AddRelativeForce(direction * magnitude, ForceMode.Acceleration);
        }

        private void ApplyRotationForces(float rollInputMultiplier, float pitchInputMultiplier, float yawInputMultiplier)
        {
            Vector3 rollComponent = _planeProperties.RollForceMultiplier * rollInputMultiplier * Vector3.forward;
            Vector3 pitchComponent = _planeProperties.PitchForceMultiplier * pitchInputMultiplier * Vector3.right;
            Vector3 yawComponent = _planeProperties.YawForceMultiplier * yawInputMultiplier * Vector3.up;
            Vector3 torque = _plane.velocity.magnitude * (rollComponent + pitchComponent + yawComponent);
            _plane.AddRelativeTorque(torque, ForceMode.Force);
        }

        private float CalculateAngleOfAttack()
        {
            Vector3 planeDir = _plane.transform.forward;
            Vector3 vel = _plane.velocity;
            Vector3 axis = _plane.transform.right;
            return Vector3.SignedAngle(planeDir, vel, axis);
        }


        private void ApplyDragForce(float AoA)
        {
            float dragCf = CalculateDragCoefficient(AoA);
            float magnitude = dragCf * _planeProperties.WingArea * 0.5f * AIR_DENSITY * Mathf.Pow(_plane.velocity.magnitude, 2);
            Vector3 direction = _plane.velocity.normalized * -1;
            _plane.AddForce(direction * magnitude, ForceMode.Force);
        }

        private float CalculateDragCoefficient(float angleOfAttack)
        {
            return (_planeProperties.MaxDragCf - _planeProperties.MinDragCf) * Mathf.Pow(Mathf.Sin(angleOfAttack), 2);
        }

        private void ApplyLiftForce(float AoA)
        {
            float liftCf = CalculateLiftCoefficient(AoA);
            float magnitude = liftCf * _planeProperties.WingArea * 0.5f * AIR_DENSITY * Mathf.Pow(_plane.velocity.magnitude, 2);
            Vector3 direction = _plane.transform.up;
            _plane.AddForce(direction * magnitude, ForceMode.Force);
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
