using System;
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



        private void Awake()
        {
            Plane = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            ApplyThrustForce();
            ApplyRotationForces();
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
    }
}
