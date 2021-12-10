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

            //Finds the relevant InputActions from PlayerInput
            rollInputAction = playerInput.actions.FindAction("Roll");
            pitchInputAction = playerInput.actions.FindAction("Pitch");
            yawInputAction = playerInput.actions.FindAction("Yaw");
        }

        private void Update()
        {
            //Reads the input values from the InputActions and feeds them to the AirplanePhysics
            airplanePhysics.rollInput = rollInputAction.ReadValue<float>();
            airplanePhysics.pitchInput = pitchInputAction.ReadValue<float>();
            airplanePhysics.yawInput = yawInputAction.ReadValue<float>();
        }
    }
}
