using System;
using UnityEngine;

namespace AirRace.Player
{
    public class AirplanePhysics : MonoBehaviour
    {
        [NonSerialized] public float rollInputMultiplier;
        [NonSerialized] public float pitchInputMultiplier;
        [NonSerialized] public float yawInputMultiplier;
        [NonSerialized] public float thrustInputMultiplier;


        [SerializeField] private Rigidbody _plane;

        [SerializeField] [Tooltip("Acceleration in m/s^2")] [Min(0)] private float _thrustAcceleration;

        [SerializeField] [Tooltip("Force multiplier on Roll axis")] [Min(0)] private float _rollForceMultiplier;
        [SerializeField] [Tooltip("Force multiplier on Pitch axis")] [Min(0)] private float _pitchForceMultiplier;
        [SerializeField] [Tooltip("Force multiplier on Yaw axis")] [Min(0)] private float _yawForceMultiplier;



        private void FixedUpdate()
        {
            ApplyThrustForce();
            ApplyRotationForces();
        }

        private void ApplyThrustForce()
        {
            float magnitude = _thrustAcceleration * thrustInputMultiplier;
            Vector3 direction = Vector3.forward;

            _plane.AddRelativeForce(direction * magnitude, ForceMode.Acceleration);
        }

        private void ApplyRotationForces()
        {
            Vector3 rollComponent = _rollForceMultiplier * rollInputMultiplier * Vector3.forward;
            Vector3 pitchComponent = _pitchForceMultiplier * pitchInputMultiplier * Vector3.right;
            Vector3 yawComponent = _yawForceMultiplier * yawInputMultiplier * Vector3.up;
            Vector3 torque = _plane.velocity.magnitude * (rollComponent + pitchComponent + yawComponent);
            _plane.AddRelativeTorque(torque, ForceMode.Force);
        }
    }
}
