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

        private Vector3 _rotateInput;
        public Vector3 RotateInput { get => _rotateInput; }
        public float ThrottleInput { get; private set; }

        public event Action PauseInputTriggered;

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
            _rotateInput = rotatePlaneAction.ReadValue<Vector2>();
            _rotateInput.z = yawAction.ReadValue<float>();
            ThrottleInput = changeThrottleAction.ReadValue<float>();
        }

        public void OnPauseUnpauseGame(InputAction.CallbackContext context)
        {
            PauseInputTriggered?.Invoke();
        }
    }
}