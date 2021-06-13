using UnityEngine;

namespace AirRace.Player
{
    public class AirplaneController : MonoBehaviour
    {
        [SerializeField] private AirplanePhysics _airplanePhysics;

        [SerializeField] [Range(0, 1)] private float _rotationInputSensivity;
        [SerializeField] [Range(0, 1)] private float _accelerateInputSensivity;


        public void OnAccelerate(float inputValue)
        {
            _airplanePhysics.thrustInputMultiplier = inputValue * _accelerateInputSensivity;
        }

        public void OnAileronsMove(float inputValue)
        {
            _airplanePhysics.rollInputMultiplier = inputValue * _rotationInputSensivity;
        }

        public void OnElevatorsMove(float inputValue)
        {
            _airplanePhysics.pitchInputMultiplier = inputValue * _rotationInputSensivity;
        }

        public void OnRudderMove(float inputValue)
        {
            _airplanePhysics.yawInputMultiplier = inputValue * _rotationInputSensivity;
        }

    }
}