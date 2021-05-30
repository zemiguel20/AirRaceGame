using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GameState
{
    public class EndGameState : State
    {
        private int score;
        private Rigidbody player;
        private UI UI;

        public EndGameState(GameManager gameManager) : base(gameManager)
        {
           // score = gameManager.raceManager.score;
            player = gameManager.player;
            UI = gameManager.UI;
        }

        public override IEnumerator Start()
        {
            player.isKinematic = true;

            UI.SetEndGamePanelActive(true);
            UI.SetEndGamePanelInfo(score);

            yield break;
        }
    }
}