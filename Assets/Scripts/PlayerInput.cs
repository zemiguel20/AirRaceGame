using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AirRace
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private InputAction rotatePlaneAction;
        [SerializeField] private InputAction yawAction;
        [SerializeField] private InputAction changeThrottleAction;
        [SerializeField] private InputAction pauseUnpauseGameAction;

        [SerializeField] private InputData inputData;

        private void OnEnable()
        {
            rotatePlaneAction.Enable();
            changeThrottleAction.Enable();
            pauseUnpauseGameAction.Enable();
            pauseUnpauseGameAction.started += OnPauseUnpauseGame;

            yawAction.Enable();
        }

        private void OnDisable()
        {
            rotatePlaneAction.Disable();
            changeThrottleAction.Disable();
            pauseUnpauseGameAction.Disable();
            pauseUnpauseGameAction.started -= OnPauseUnpauseGame;

            yawAction.Disable();
        }

        private void Update()
        {
            inputData.RotateValues.x = rotatePlaneAction.ReadValue<Vector2>().x;
            inputData.RotateValues.y = rotatePlaneAction.ReadValue<Vector2>().y;
            inputData.RotateValues.z = yawAction.ReadValue<float>();
            inputData.ThrottleValue = changeThrottleAction.ReadValue<float>();
        }

        public void OnPauseUnpauseGame(InputAction.CallbackContext context)
        {
            inputData.InvokePauseTriggeredEvent();
        }
    }
}