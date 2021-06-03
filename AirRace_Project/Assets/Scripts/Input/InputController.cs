using AirRace.GameState;
using AirRace.Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{

    [SerializeField] private MovementController _planeMovement;
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

    public void OnPauseGame(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (GameManager.isPaused)
                _gameManager.ResumeGame();
            else
                _gameManager.PauseGame();
        }

    }
}
