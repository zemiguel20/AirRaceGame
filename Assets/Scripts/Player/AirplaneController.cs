using AirRace.Core;
using UnityEngine;

namespace AirRace.Player
{
    [RequireComponent(typeof(AirplanePhysics))]
    public class AirplaneController : MonoBehaviour
    {
        public delegate void TerrainHitHandler();
        public event TerrainHitHandler TerrainHit;

        // TODO -  move sensivity to input
        [SerializeField] [Range(0, 1)] private float _rotationInputSensivity;
        [SerializeField] [Range(0, 1)] private float _accelerateInputSensivity;

        private AirplanePhysics _airplanePhysics;

        private void Awake()
        {
            _airplanePhysics = GetComponent<AirplanePhysics>();
        }

        private void OnCollisionEnter()
        {
            TerrainHit.Invoke();
        }

        public void OnAccelerate(float inputValue)
        {
            _airplanePhysics.ThrustInputMultiplier = inputValue * _accelerateInputSensivity;
        }

        public void OnAileronsMove(float inputValue)
        {
            _airplanePhysics.RollInputMultiplier = inputValue * _rotationInputSensivity;
        }

        public void OnElevatorsMove(float inputValue)
        {
            _airplanePhysics.PitchInputMultiplier = inputValue * _rotationInputSensivity;
        }

        public void OnRudderMove(float inputValue)
        {
            _airplanePhysics.YawInputMultiplier = inputValue * _rotationInputSensivity;
        }

        public void EnablePhysics(bool value)
        {
            _airplanePhysics.Plane.isKinematic = !value;
        }

        public PositionRotationTuple PlanePositionAndRotation()
        {
            return new PositionRotationTuple(_airplanePhysics.Plane.position, _airplanePhysics.Plane.rotation);
        }

        public void SetPlanePositionAndRotation(PositionRotationTuple tuple)
        {
            _airplanePhysics.Plane.position = tuple.Position;
            _airplanePhysics.Plane.rotation = tuple.Rotation;
        }

    }
}