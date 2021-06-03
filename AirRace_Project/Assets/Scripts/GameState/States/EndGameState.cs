using AirRace.Core;
using AirRace.UI.Race;
using System.Collections;

namespace AirRace.GameState.States
{
    public class EndGameState : State
    {
        private float playerTime;
        private UIManager _UI;

        public EndGameState(GameManager gameManager, float time) : base(gameManager)
        {
            playerTime = time;
            _UI = gameManager.GetUIManager();
        }

        public override IEnumerator Start()
        {
            GameLogger.Debug("Race Finished: " + playerTime);

            _UI.SetEndGamePanelActive(true);
            _UI.SetEndGamePanelInfo(playerTime);

            yield break;
        }
    }
}