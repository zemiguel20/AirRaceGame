using AirRace.Utils;
using AirRace.Player;
using UnityEngine;
using System;
using System.Collections;

namespace AirRace.Race
{
    public class RaceController : MonoBehaviour
    {
        private Timer _countdownTimer;
        public Timer Timer { get => _countdownTimer; }

        private Airplane _airplane;
        public Airplane Airplane { get => _airplane; }

        private Path _path;

        private Chronometer _chronometer;
        public Chronometer Chronometer { get => _chronometer; }

        private PlayerRespawner _respawner;

        private Leaderboard _leaderboard;

        private IPlayerInput _playerInput;

        private bool _isPaused = false;
        private bool _isRacing = false;


        public event Action CountdownStarted;
        public event Action CountdownFinished;
        public event Action RaceStarted;
        public event Action RaceEnded;
        public event Action GamePaused;
        public event Action GameResumed;

        //Called every frame update
        private void Update()
        {
            if (_isRacing) _chronometer.Tick(Time.deltaTime);
        }

        public void Initialize(Airplane player, Path path, Leaderboard leaderboard, IPlayerInput playerInput)
        {
            _airplane = player;
            _path = path;
            _leaderboard = leaderboard;
            _playerInput = playerInput;

            _countdownTimer = new Timer(5);
            _chronometer = new Chronometer();
            _respawner = new PlayerRespawner(_airplane);
        }

        public void StartRace()
        {
            StartCoroutine(StartCountdownPhase());
        }

        private IEnumerator StartCountdownPhase()
        {
            _airplane.EnablePhysics(false);

            CountdownStarted?.Invoke();
            while (_countdownTimer.IsFinished == false)
            {
                yield return new WaitForSeconds(1);
                _countdownTimer.TickSeconds(1);
                GameLogger.Debug(_countdownTimer.RemaingSeconds.ToString());
            }
            CountdownFinished?.Invoke();

            StartRacePhase();
        }

        private void StartRacePhase()
        {
            GameLogger.Debug("Race Started!");

            _path.Initialize();

            _airplane.GoalHit += OnGoalPassed;
            _airplane.TerrainHit += OnPlayerTerrainHit;
            _playerInput.PauseInputTriggered += PauseResumeGame;

            _isRacing = true;
            _airplane.EnablePhysics(true);

            RaceStarted?.Invoke();
        }

        private void OnGoalPassed(GameObject goal)
        {
            GameLogger.Debug("Passed goal");
            _path.NextGoal();

            _respawner.UpdateRespawn(goal);

            if (_path.IsFinished()) EndRacePhase();
        }

        private void EndRacePhase()
        {
            GameLogger.Debug("Race Finished: " + _chronometer.Time);

            _airplane.EnablePhysics(false);
            _isRacing = false;
            _leaderboard.AddEntry(_chronometer.Time);
            RaceEnded?.Invoke();

            _playerInput.PauseInputTriggered -= PauseResumeGame;
        }

        private void OnPlayerTerrainHit()
        {
            StartCoroutine(RespawnSequence());
        }

        private IEnumerator RespawnSequence()
        {
            _airplane.EnablePhysics(false);
            yield return new WaitForSeconds(0.3f);
            _respawner.Respawn();
            yield return new WaitForSeconds(0.3f);
            _airplane.EnablePhysics(true);
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

    }
}