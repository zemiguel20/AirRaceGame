using AirRace.Core.Events;
using AirRace.GameState.States;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AirRace.GameState
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private EventManager _eventManager;

        //public UI UI { get; private set; }

        public static bool isPaused = false;

        private State state;

        public EventManager GetEventManager()
        {
            return _eventManager;
        }

        private void Start()
        {
            _eventManager.RaceEnded += OnRaceEnd;

            SetState(new InitialCountdownState(this));
        }

        private void OnDisable()
        {
            _eventManager.RaceEnded -= OnRaceEnd;
        }

        public void SetState(State newState)
        {
            state = newState;
            StartCoroutine(state.Start());
        }

        public void OnRaceEnd(float time)
        {
            SetState(new EndGameState(this, time));
        }


        public void ExitToMenu()
        {
            Time.timeScale = 1;
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

    }
}