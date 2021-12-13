using System.Text;
using AirRace.Race;
using TMPro;
using UnityEngine;

namespace AirRace.UI
{
    public class EndGamePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private LeaderboardUI _leaderboardUI;
        private Leaderboard _leaderboard;
        private RaceController _raceController;

        public void Initialize(RaceController raceController, Leaderboard leaderboard)
        {
            gameObject.SetActive(false);

            _raceController = raceController;
            _leaderboard = leaderboard;

            // _raceController.RaceEnded += ShowPanel;
        }

        public void ShowPanel()
        {
            // _timeText.text = _raceController.Chronometer.Time.ToString("F2");
            _leaderboardUI.SetLeaderboard(_leaderboard);

            gameObject.SetActive(true);
        }

        public void OnExitButtonPressed()
        {
            //_raceController.ExitRace();
        }

        public void OnRestartButtonPressed()
        {
            //_raceController.RestartRace();
        }
    }
}