using AirRace.Race;
using AirRace.Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{

    [SerializeField] private AirplaneController _planeMovement;
    [SerializeField] private GameManager _gameManager;

    [SerializeField] [Range(0, 1)] private float _rotationInputSensivity;
    [SerializeField] [Range(0, 1)] private float _accelerateInputSensivity;

    public void OnAccelerate(InputAction.CallbackContext context)
    {
        _planeMovement.OnAccelerate(context.ReadValue<float>() * _accelerateInputSensivity);
    }

    public void OnAileronsMove(InputAction.CallbackContext context)
    {
        _planeMovement.OnAileronsMove(context.ReadValue<float>() * _rotationInputSensivity);
    }

    public void OnElevatorsMove(InputAction.CallbackContext context)
    {
        _planeMovement.OnElevatorsMove(context.ReadValue<float>() * _rotationInputSensivity);
    }

    public void OnRudderMove(InputAction.CallbackContext context)
    {
        _planeMovement.OnRudderMove(context.ReadValue<float>() * _rotationInputSensivity);
    }

    public void OnPauseUnpauseGame(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _gameManager.PauseResumeGame();
        }
    }
}
