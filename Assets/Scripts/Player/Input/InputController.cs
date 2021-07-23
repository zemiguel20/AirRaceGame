using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AirRace.Player
{
    public class InputController : MonoBehaviour, IPlayerInput
    {
        [SerializeField] [Range(0, 1)] private float _rotationInputSensivity;
        [SerializeField] [Range(0, 1)] private float _accelerateInputSensivity;


        public float RollInputMultiplier { get; private set; }
        public float PitchInputMultiplier { get; private set; }
        public float YawInputMultiplier { get; private set; }
        public float AccelerateInputMultiplier { get; private set; }

        public event Action PauseInputTriggered;

        public void OnAccelerate(InputAction.CallbackContext context)
        {
            AccelerateInputMultiplier = context.ReadValue<float>() * _accelerateInputSensivity;
        }

        public void OnAileronsMove(InputAction.CallbackContext context)
        {
            RollInputMultiplier = context.ReadValue<float>() * _rotationInputSensivity;
        }

        public void OnElevatorsMove(InputAction.CallbackContext context)
        {
            PitchInputMultiplier = context.ReadValue<float>() * _rotationInputSensivity;
        }

        public void OnRudderMove(InputAction.CallbackContext context)
        {
            YawInputMultiplier = context.ReadValue<float>() * _rotationInputSensivity;
        }

        public void OnPauseUnpauseGame(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                PauseInputTriggered?.Invoke();
            }
        }
    }
}