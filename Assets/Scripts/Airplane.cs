using System;
using AirRace.Utils;
using UnityEngine;

namespace AirRace.Player
{

    [RequireComponent(typeof(Rigidbody))]
    public class Airplane : MonoBehaviour
    {
        [SerializeField] private AirplaneProperties _planeProperties;
        [SerializeField] private PlayerInput _playerInput;

        private Rigidbody _rigidbody;
        private AirplanePhysics _airplanePhysics;

        public event Action<GameObject> GoalHit;
        public event Action TerrainHit;

        private float _throttleMultiplier = 0;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _airplanePhysics = new AirplanePhysics(_rigidbody, _planeProperties);
        }

        private void FixedUpdate()
        {
            float rollInputMultiplier = -_playerInput.RotateInput.x;
            float pitchInputMultiplier = _playerInput.RotateInput.y;
            float yawInputMultiplier = 0;
            _airplanePhysics.UpdateForces(_throttleMultiplier, rollInputMultiplier, pitchInputMultiplier, yawInputMultiplier);
        }

        private void Update()
        {
            _throttleMultiplier += _playerInput.ThrottleInput * Time.deltaTime * _planeProperties.ThrottleIncreasePerSecond;
            _throttleMultiplier = Mathf.Clamp01(_throttleMultiplier);
        }

        private void OnCollisionEnter()
        {
            TerrainHit?.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Goal")) GoalHit?.Invoke(other.gameObject);
        }

        public void EnablePhysics(bool value)
        {
            _rigidbody.isKinematic = !value;
        }

        public Vector3 Position { get => _rigidbody.position; set => _rigidbody.position = value; }
        public Quaternion Rotation { get => _rigidbody.rotation; set => _rigidbody.rotation = value; }
        public Vector3 Velocity { get => _rigidbody.velocity; }
        public float Throttle { get => _throttleMultiplier; }
    }
}