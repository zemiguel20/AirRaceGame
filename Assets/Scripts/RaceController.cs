using AirRace.Utils;
using UnityEngine;
using System;
using System.Collections;

namespace AirRace.Race
{
    public class RaceController : MonoBehaviour
    {
        private Timer _countdownTimer;
        public Timer Timer { get => _countdownTimer; }

        //private AirplaneMovement _airplane;
        //public AirplaneMovement Airplane { get => _airplane; }

        private Path _path;
        public Path Path { get => _path; }

        private Chronometer _chronometer;
        public Chronometer Chronometer { get => _chronometer; }

        private Leaderboard _leaderboard;

       // private PlayerInput _playerInput;

        private bool _isPaused = false;
        private bool _isRacing = false;


        public event Action CountdownStarted;
        public event Action CountdownFinished;
        public event Action RaceStarted;
        public event Action RaceEnded;
        public event Action GamePaused;
        public event Action GameResumed;
        public event Action RaceExited;
        public event Action RaceRestarted;

        //Called every frame update
        private void Update()
        {
            if (_isRacing) _chronometer.Tick(Time.deltaTime);
        }

        public void Initialize(/*AirplaneMovement player,*/ Path path, Leaderboard leaderboard/*, PlayerInput playerInput*/)
        {
           // _airplane = player;
            _path = path;
            _leaderboard = leaderboard;
           // _playerInput = playerInput;

            _countdownTimer = new Timer(5);
            _chronometer = new Chronometer();
        }

        public void StartRace()
        {
            StartCoroutine(StartCountdownPhase());
        }

        private IEnumerator StartCountdownPhase()
        {
          //  _airplane.SetEnabled(false);

            CountdownStarted?.Invoke();
            while (_countdownTimer.IsFinished == false)
            {
                yield return new WaitForSeconds(1);
                _countdownTimer.TickSeconds(1);
                Debug.Log(_countdownTimer.RemaingSeconds.ToString());
            }
            CountdownFinished?.Invoke();

            StartRacePhase();
        }

        private void StartRacePhase()
        {
            Debug.Log("Race Started!");

            _path.Initialize();

            //_airplane.GoalHit += OnGoalPassed;
            // _playerInput.PauseInputTriggered += PauseResumeGame;

            _isRacing = true;
            //_airplane.SetEnabled(true);

            RaceStarted?.Invoke();
        }

        private void OnGoalPassed(GameObject goal)
        {
            Debug.Log("Passed goal");
            _path.NextGoal();

            if (_path.IsFinished()) EndRacePhase();
        }

        private void EndRacePhase()
        {
            Debug.Log("Race Finished: " + _chronometer.Time);

           // _airplane.SetEnabled(false);
            _isRacing = false;
            _leaderboard.AddEntry(_chronometer.Time);
            RaceEnded?.Invoke();

            // _playerInput.PauseInputTriggered -= PauseResumeGame;
        }

        public void PauseResumeGame()
        {
            if (_isPaused)
            {
                Time.timeScale = 1;
                _isPaused = false;
                GameResumed?.Invoke();
            }
            else
            {
                Time.timeScale = 0;
                _isPaused = true;
                GamePaused?.Invoke();
            }
        }

        public void ExitRace()
        {
            Time.timeScale = 1;
            RaceExited?.Invoke();
        }

        public void RestartRace()
        {
            Time.timeScale = 1;
            RaceRestarted?.Invoke();
        }

    }
}