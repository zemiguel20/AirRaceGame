using UnityEngine;
using UnityEngine.InputSystem;

namespace AirRace.Input
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] [Range(0, 1)] private float _rotationInputSensivity;
        [SerializeField] [Range(0, 1)] private float _accelerateInputSensivity;

        public delegate void AccelerateHandler(float value);
        public event AccelerateHandler AccelerateInputTriggered;

        public delegate void AileronsHandler(float value);
        public event AileronsHandler AileronsInputTriggered;

        public delegate void ElevatorsHandler(float value);
        public event ElevatorsHandler ElevatorsInputTriggered;

        public delegate void RudderHandler(float value);
        public event RudderHandler RudderInputTriggered;

        public delegate void PauseHandler();
        public event PauseHandler PauseInputTriggered;


        public void OnAccelerate(InputAction.CallbackContext context)
        {
            AccelerateInputTriggered?.Invoke(context.ReadValue<float>() * _accelerateInputSensivity);
        }

        public void OnAileronsMove(InputAction.CallbackContext context)
        {
            AileronsInputTriggered?.Invoke(context.ReadValue<float>() * _rotationInputSensivity);
        }

        public void OnElevatorsMove(InputAction.CallbackContext context)
        {
            ElevatorsInputTriggered?.Invoke(context.ReadValue<float>() * _rotationInputSensivity);
        }

        public void OnRudderMove(InputAction.CallbackContext context)
        {
            RudderInputTriggered?.Invoke(context.ReadValue<float>() * _rotationInputSensivity);
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