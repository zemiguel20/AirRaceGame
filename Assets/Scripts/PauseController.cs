using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AirRace
{
    public class PauseController : MonoBehaviour
    {
        //True - paused
        //False - running
        public static event Action<bool> pauseStateChanged;

        private PlayerInput playerInput;

        private void Awake()
        {
            playerInput = FindObjectOfType<PlayerInput>();
            //There should be 2 input action maps, one with actions like player controls
            //and a Pause game action while the game is running, and another one
            //with actions the player can do while the game is paused,
            //mainly the resume game action.
            playerInput.actions.FindAction("PauseGame").performed += OnPauseGame;
            playerInput.actions.FindAction("ResumeGame").performed += OnResumeGame;
        }

        //Callback for input action
        private void OnPauseGame(InputAction.CallbackContext context)
        {
            PauseGame();
        }

        //Callback for input action
        private void OnResumeGame(InputAction.CallbackContext context)
        {
            ResumeGame();
        }

        public void PauseGame()
        {
            //Switches to the action map for the paused game
            playerInput.SwitchCurrentActionMap("GamePaused");
            Time.timeScale = 0;

            pauseStateChanged?.Invoke(true);
        }

        public void ResumeGame()
        {
            //switches to the action map for the running game
            playerInput.SwitchCurrentActionMap("Player");
            Time.timeScale = 1;

            pauseStateChanged?.Invoke(false);
        }

        //Cleanup on object destroy
        private void OnDestroy()
        {
            //Reset timescale
            Time.timeScale = 1;

            //Unsub from events
            playerInput.actions.FindAction("PauseGame").performed -= OnPauseGame;
            playerInput.actions.FindAction("ResumeGame").performed -= OnResumeGame;
        }
    }
}
