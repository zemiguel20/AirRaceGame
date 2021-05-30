using AirRace.GameManager.States;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace AirRace.GameManager
{
    public class GameManager : MonoBehaviour
    {
        public int initialCountdown;

        public Rigidbody player { get; private set; }
        //public RaceManager raceManager { get; private set; }

        public UI UI { get; private set; }


        private State state;

        public static bool isPaused = false;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
            //raceManager = GetComponent<RaceManager>();
            UI = GameObject.Find("UI").GetComponent<UI>();
        }

        private void Start()
        {
            SetState(new InitialCountdownState(this));
        }

        public void SetState(State newState)
        {
            state = newState;
            StartCoroutine(state.Start());
        }


        public void ExitToMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void RestartGame()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void PauseGame()
        {
            StartCoroutine(state.Pause());
        }

        public void ResumeGame()
        {
            StartCoroutine(state.Resume());
        }

        public void OnPauseGame(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                if (isPaused)
                    ResumeGame();
                else
                    PauseGame();
            }

        }
    }
}