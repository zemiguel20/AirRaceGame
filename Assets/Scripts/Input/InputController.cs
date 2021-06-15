using AirRace.Race;
using AirRace.Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{

    [SerializeField] private AirplaneController _planeMovement;
    [SerializeField] private GameManager _gameManager;

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

    public void OnPauseUnpauseGame(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _gameManager.PauseResumeGame();
        }
    }
}
