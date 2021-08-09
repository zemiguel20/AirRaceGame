using System;
using AirRace.Utils;
using UnityEngine;

namespace AirRace.Player
{

    public class Airplane : MonoBehaviour
    {
        [SerializeField] private Rigidbody _plane;
        [SerializeField] private PlanePropertiesSO _planeProperties;
        [SerializeField] private PlayerInput _playerInput;

        public event Action<GameObject> GoalHit;
        public event Action TerrainHit;

        private AirplanePhysics _airplanePhysics;

        private float _throttleMultiplier = 0;

        public void Initialize(PlayerInput inputController)
        {
            _airplanePhysics = new AirplanePhysics(_plane, _planeProperties);
            _playerInput = inputController;
        }

        private void FixedUpdate()
        {
            float rollInputMultiplier = _playerInput.RollInputMultiplier;
            float pitchInputMultiplier = _playerInput.PitchInputMultiplier;
            float yawInputMultiplier = _playerInput.YawInputMultiplier;
            _airplanePhysics.UpdateForces(_throttleMultiplier, rollInputMultiplier, pitchInputMultiplier, yawInputMultiplier);
        }

        private void Update()
        {
            if (_playerInput != null)
            {
                _throttleMultiplier += _playerInput.AccelerateInputMultiplier * Time.deltaTime * _planeProperties.ThrottleIncreasePerSecond;
                _throttleMultiplier = Mathf.Clamp01(_throttleMultiplier);
            }
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
            _plane.isKinematic = !value;
        }

        public Vector3 Position { get => _plane.position; set => _plane.position = value; }
        public Quaternion Rotation { get => _plane.rotation; set => _plane.rotation = value; }
        public Vector3 Velocity { get => _plane.velocity; }
        public float Throttle { get => _throttleMultiplier; }
    }
}