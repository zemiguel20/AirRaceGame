using AirRace.Core;
using AirRace.Core.SOs;
using AirRace.UI.Race;
using System.Collections;

namespace AirRace.GameState.States
{
    public class EndGameState : State
    {
        private float playerTime;
        private UIManager _UI;
        private LeaderboardSO _leaderboard;

        public EndGameState(GameManager gameManager, float time) : base(gameManager)
        {
            playerTime = time;
            _UI = gameManager.GetUIManager();
            _leaderboard = gameManager.GetLeaderboard();
        }

        public override IEnumerator Start()
        {
            GameLogger.Debug("Race Finished: " + playerTime);

            //Save leaderboard
            _leaderboard.AddEntry(playerTime);
            SaveManager.SaveLeaderboard(_leaderboard.ToSerializable(), _leaderboard.name);


            _UI.SetEndGamePanelActive(true);
            _UI.SetEndGamePanelInfo(playerTime);

            yield break;
        }
    }
}