using UnityEngine;
using UnityEngine.InputSystem;

namespace AirRace
{
    public class AirplaneController : MonoBehaviour
    {
        private PlayerInput playerInput;
        private AirplanePhysics airplanePhysics;

        private InputAction rollInputAction;
        private InputAction pitchInputAction;
        private InputAction yawInputAction;

        private void Awake()
        {
            playerInput = FindObjectOfType<PlayerInput>();
            airplanePhysics = GetComponent<AirplanePhysics>();

            rollInputAction = playerInput.actions.FindAction("Roll");
            pitchInputAction = playerInput.actions.FindAction("Pitch");
            yawInputAction = playerInput.actions.FindAction("Yaw");
        }

        private void Update()
        {
            airplanePhysics.rollInput = rollInputAction.ReadValue<float>();
            airplanePhysics.pitchInput = pitchInputAction.ReadValue<float>();
            airplanePhysics.yawInput = yawInputAction.ReadValue<float>();
        }
    }
}
