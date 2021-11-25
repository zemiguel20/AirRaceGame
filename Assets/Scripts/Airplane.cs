using System;
using UnityEngine;

namespace AirRace
{

    [RequireComponent(typeof(Rigidbody))]
    public class Airplane : MonoBehaviour
    {

        [SerializeField] [Min(0)] private float _maxThrottle;
        [SerializeField] [Min(0)] private float _rollSpeed;
        [SerializeField] [Min(0)] private float _pitchSpeed;
        [SerializeField] [Min(0)] private float _yawSpeed;
        [SerializeField] [Min(0)] private float _dragCoefficient;

        [Space(10)]

        [SerializeField] private InputData inputData;

        private Rigidbody _rigidbody;


        private float _throttle = 0;
        private const float THROTTLE_INCREASE_PER_SEC = 5;


        public event Action<GameObject> GoalHit;
        public event Action TerrainHit;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _throttle += inputData.ThrottleValue * Time.deltaTime * THROTTLE_INCREASE_PER_SEC;
            _throttle = Mathf.Clamp01(_throttle);
        }

        private void FixedUpdate()
        {
            UpdatePlaneRotation();
            UpdateForces();
        }

        private void UpdatePlaneRotation()
        {
            Vector3 input = inputData.RotateValues;

            float velocityFactor = Mathf.Log(_rigidbody.velocity.magnitude, 30);
            if (velocityFactor == float.NegativeInfinity) { velocityFactor = 0; }

            Vector3 rollAxis = transform.forward;
            Vector3 pitchAxis = transform.right;
            Vector3 yawAxis = transform.up;
            Vector3 rollRotation = -input.x * _rollSpeed * velocityFactor * rollAxis;
            Vector3 pitchRotation = input.y * _pitchSpeed * velocityFactor * pitchAxis;
            Vector3 yawRotation = input.z * _yawSpeed * velocityFactor * yawAxis;


            _rigidbody.angularVelocity = Vector3.Lerp(_rigidbody.angularVelocity, rollRotation + pitchRotation + yawRotation, 0.5f);
        }

        private void UpdateForces()
        {
            ApplyThrustForce();
            ApplyDragForce();
        }
        private void ApplyThrustForce()
        {
            float magnitude = _maxThrottle * _throttle;
            Vector3 direction = transform.forward;
            _rigidbody.AddForce(_rigidbody.mass * magnitude * direction, ForceMode.Force);
        }

        private void ApplyDragForce()
        {
            float magnitude = _dragCoefficient * Mathf.Pow(_rigidbody.velocity.magnitude, 2);
            Vector3 direction = _rigidbody.velocity.normalized * -1;
            _rigidbody.AddForce(direction * magnitude, ForceMode.Force);
        }


        private void OnCollisionEnter()
        {
            TerrainHit?.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            GoalHit?.Invoke(other.gameObject);
        }

        public void EnablePhysics(bool value)
        {
            _rigidbody.isKinematic = !value;
        }

        public Rigidbody Rigidbody { get => _rigidbody; }
        public Vector3 Velocity { get => _rigidbody.velocity; }
        public float Throttle { get => _throttle; }
    }
}