using AirRace.Utils;
using AirRace.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using AirRace.Leaderboard;

namespace AirRace.Race
{
    public class RaceController : MonoBehaviour
    {
        [SerializeField] private LeaderboardSO _leaderboard;
        [SerializeField] private AirplaneController _airplaneController;
        [SerializeField] private Chronometer _chronometer;
        [SerializeField] private PathManager _pathManager;

        private Timer _timer;
        private bool _isPaused = false;


        #region Getters
        public LeaderboardSO Leaderboard { get => _leaderboard; }
        public Timer Timer { get => _timer; }
        public AirplaneController AirplaneController { get => _airplaneController; }
        public Chronometer Chronometer { get => _chronometer; }
        public PathManager PathManager { get => _pathManager; }
        #endregion

        #region Events
        public delegate void RaceEndHandler();
        public event RaceEndHandler RaceEnded;

        public delegate void GamePausedHandler();
        public event GamePausedHandler GamePaused;

        public delegate void GameResumedHandler();
        public event GameResumedHandler GameResumed;
        #endregion


        private void OnDisable()
        {
            Timer.TimerEnded -= StartRace;

            foreach (Goal goal in _pathManager.Goals)
            {
                goal.GoalPassed -= OnGoalPassed;
            }
        }

        private void Awake()
        {
            _timer = new Timer();
        }

        private void Start()
        {
            Timer.TimerEnded += StartRace;

            foreach (Goal goal in _pathManager.Goals)
            {
                goal.GoalPassed += OnGoalPassed;
            }

            StartCountdown();
        }

        #region Race Sequence
        private void StartCountdown()
        {
            _airplaneController.EnablePhysics(false);

            StartCoroutine(_timer.StartTimer(5));
        }

        private void StartRace()
        {
            GameLogger.Debug("Race Started!");

            _chronometer.StartChrono();
            _pathManager.StartPath();

            _airplaneController.EnablePhysics(true);
        }

        private void OnGoalPassed(Goal goal)
        {
            _pathManager.ChangeActiveGoal();

            if (_pathManager.IsFinished())
            {
                EndRace();
            }
        }

        private void EndRace()
        {
            _chronometer.StopChrono();
            _airplaneController.EnablePhysics(false);

            GameLogger.Debug("Race Finished: " + _chronometer.time);

            //Save leaderboard
            _leaderboard.AddEntry(_chronometer.time);
            SaveManager.SaveLeaderboard(_leaderboard.ToSerializable(), _leaderboard.name);

            RaceEnded?.Invoke();
        }
        #endregion



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