using AirRace.Core;
using UnityEngine;

namespace AirRace.Player
{
    [RequireComponent(typeof(AirplanePhysics))]
    public class AirplaneController : MonoBehaviour
    {
        public delegate void TerrainHitHandler();
        public event TerrainHitHandler TerrainHit;
        private AirplanePhysics _airplanePhysics;

        private void Awake()
        {
            _airplanePhysics = GetComponent<AirplanePhysics>();
        }

        private void OnCollisionEnter()
        {
            TerrainHit?.Invoke();
        }

        public void OnAccelerate(float inputValue)
        {
            _airplanePhysics.ThrustInputMultiplier = inputValue;
        }

        public void OnAileronsMove(float inputValue)
        {
            _airplanePhysics.RollInputMultiplier = inputValue;
        }

        public void OnElevatorsMove(float inputValue)
        {
            _airplanePhysics.PitchInputMultiplier = inputValue;
        }

        public void OnRudderMove(float inputValue)
        {
            _airplanePhysics.YawInputMultiplier = inputValue;
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