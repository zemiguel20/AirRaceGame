using UnityEngine;

namespace AirRace
{
    public class AirplaneMovement : MonoBehaviour
    {
        [SerializeField] [Min(0)] private float _throttle;
        [SerializeField] [Min(0)] private float _rollSpeed;
        [SerializeField] [Min(0)] private float _pitchSpeed;
        [SerializeField] [Min(0)] private float _yawSpeed;
        [SerializeField] [Min(0)] private float _dragCoefficient;

        [Space(10)]

        [SerializeField] private InputData inputData;
        [SerializeField] private Rigidbody airplaneRigidbody;

        private void FixedUpdate()
        {
            UpdatePlaneRotation();
            UpdateForces();
        }

        private void UpdatePlaneRotation()
        {
            Vector3 input = inputData.RotateValues;

            float velocityFactor = Mathf.Log(airplaneRigidbody.velocity.magnitude, 30);
            if (velocityFactor == float.NegativeInfinity) { velocityFactor = 0; }

            Vector3 rollAxis = transform.forward;
            Vector3 pitchAxis = transform.right;
            Vector3 yawAxis = transform.up;
            Vector3 rollRotation = -input.x * _rollSpeed * velocityFactor * rollAxis;
            Vector3 pitchRotation = input.y * _pitchSpeed * velocityFactor * pitchAxis;
            Vector3 yawRotation = input.z * _yawSpeed * velocityFactor * yawAxis;


            airplaneRigidbody.angularVelocity = Vector3.Lerp(airplaneRigidbody.angularVelocity, rollRotation + pitchRotation + yawRotation, 0.5f);
        }

        private void UpdateForces()
        {
            ApplyThrustForce();
            ApplyDragForce();
        }
        private void ApplyThrustForce()
        {
            float magnitude = _throttle * airplaneRigidbody.mass;
            Vector3 direction = transform.forward;
            airplaneRigidbody.AddForce(magnitude * direction);
        }

        private void ApplyDragForce()
        {
            float magnitude = _dragCoefficient * Mathf.Pow(airplaneRigidbody.velocity.magnitude, 2);
            Vector3 direction = airplaneRigidbody.velocity.normalized * -1;
            airplaneRigidbody.AddForce(direction * magnitude);
        }

        public void SetEnabled(bool value)
        {
            airplaneRigidbody.isKinematic = !value;
            this.enabled = value;
        }
    }
}