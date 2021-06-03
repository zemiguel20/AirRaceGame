using AirRace.Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{

    [SerializeField] private MovementController _planeMovement;

    public void OnAccelerate(InputAction.CallbackContext context)
    {
        _planeMovement.OnAccelerate(context.ReadValue<float>());
    }

    public void OnAileronsMove(InputAction.CallbackContext context)
    {
        _planeMovement.OnAileronsMove(context.ReadValue<float>());
    }

    public void OnElevatorsMove(InputAction.CallbackContext context)
    {
        _planeMovement.OnElevatorsMove(context.ReadValue<float>());
    }
}
