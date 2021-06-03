using AirRace.Core;
using System.Collections;

namespace AirRace.GameState.States
{
    public class EndGameState : State
    {
        private float playerTime;
        //private UI UI;

        public EndGameState(GameManager gameManager, float time) : base(gameManager)
        {
            playerTime = time;
            //UI = gameManager.UI;
        }

        public override IEnumerator Start()
        {
            GameLogger.Debug("Race Finished: " + playerTime);

            // UI.SetEndGamePanelActive(true);
            // UI.SetEndGamePanelInfo(score);

            yield break;
        }
    }
}