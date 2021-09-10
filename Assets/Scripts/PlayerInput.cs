using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AirRace
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private InputAction rotatePlaneAction;
        [SerializeField] private InputAction changeThrottleAction;
        [SerializeField] private InputAction pauseUnpauseGameAction;

        public Vector2 RotateInput { get; private set; }
        public float ThrottleInput { get; private set; }

        public event Action PauseInputTriggered;

        private void OnEnable()
        {
            rotatePlaneAction.Enable();
            changeThrottleAction.Enable();
            pauseUnpauseGameAction.Enable();
            pauseUnpauseGameAction.started += OnPauseUnpauseGame;
        }

        private void OnDisable()
        {
            rotatePlaneAction.Disable();
            changeThrottleAction.Disable();
            pauseUnpauseGameAction.Disable();
            pauseUnpauseGameAction.started -= OnPauseUnpauseGame;
        }

        private void Update()
        {
            RotateInput = rotatePlaneAction.ReadValue<Vector2>();
            ThrottleInput = changeThrottleAction.ReadValue<float>();
        }

        public void OnPauseUnpauseGame(InputAction.CallbackContext context)
        {
            PauseInputTriggered?.Invoke();
        }
    }
}