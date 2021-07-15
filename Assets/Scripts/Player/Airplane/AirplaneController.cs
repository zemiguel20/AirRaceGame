using AirRace.Utils;
using UnityEngine;

namespace AirRace.Player
{

    public class AirplaneController : MonoBehaviour
    {

        [SerializeField] private Rigidbody _plane;
        [SerializeField] private PlanePropertiesSO _planeProperties;
        [SerializeField] private InputController _inputController;

        [SerializeField] [Range(0, 1)] private float THROTTLE_INCREASE_PER_SEC;

        public delegate void TerrainHitHandler();
        public event TerrainHitHandler TerrainHit;

        private AirplanePhysics _airplanePhysics;

        private float _rollInputMultiplier;
        private float _pitchInputMultiplier;
        private float _yawInputMultiplier;

        private float _accelerateInputMultiplier;
        private float _throttleMultiplier;

        private void Start()
        {
            _airplanePhysics = new AirplanePhysics(_plane, _planeProperties);

            _inputController.AccelerateInputTriggered += OnAccelerate;
            _inputController.AileronsInputTriggered += OnAileronsMove;
            _inputController.ElevatorsInputTriggered += OnElevatorsMove;
            _inputController.RudderInputTriggered += OnRudderMove;
        }

        private void OnDisable()
        {
            _inputController.AccelerateInputTriggered -= OnAccelerate;
            _inputController.AileronsInputTriggered -= OnAileronsMove;
            _inputController.ElevatorsInputTriggered -= OnElevatorsMove;
            _inputController.RudderInputTriggered -= OnRudderMove;
        }

        public void OnAccelerate(float inputValue)
        {
            _accelerateInputMultiplier = inputValue;
        }

        public void OnAileronsMove(float inputValue)
        {
            _rollInputMultiplier = inputValue;
        }

        public void OnElevatorsMove(float inputValue)
        {
            _pitchInputMultiplier = inputValue;
        }

        public void OnRudderMove(float inputValue)
        {
            _yawInputMultiplier = inputValue;
        }

        private void FixedUpdate()
        {
            _airplanePhysics.UpdateForces(_throttleMultiplier, _rollInputMultiplier, _pitchInputMultiplier, _yawInputMultiplier);
        }

        private void Update()
        {
            _throttleMultiplier += _accelerateInputMultiplier * Time.deltaTime * THROTTLE_INCREASE_PER_SEC;
            _throttleMultiplier = Mathf.Clamp01(_throttleMultiplier);
        }


        private void OnCollisionEnter()
        {
            TerrainHit?.Invoke();
        }

        public void EnablePhysics(bool value)
        {
            _plane.isKinematic = !value;
        }

        public PositionRotationTuple PlanePositionAndRotation()
        {
            return new PositionRotationTuple(_plane.position, _plane.rotation);
        }

        public void SetPlanePositionAndRotation(PositionRotationTuple tuple)
        {
            _plane.position = tuple.Position;
            _plane.rotation = tuple.Rotation;
        }

    }
}