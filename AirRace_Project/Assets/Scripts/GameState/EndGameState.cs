using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GameState
{
    public class EndGameState : State
    {
        private int score;
        private Rigidbody player;
        private EndGamePanel endGamePanel;

        public EndGameState(GameManager gameManager) : base(gameManager)
        {
            score = gameManager.raceManager.score;
            player = gameManager.player;
            endGamePanel = gameManager.endGamePanel;
        }

        public override IEnumerator Start()
        {
            player.isKinematic = true;

            endGamePanel.gameObject.SetActive(true);
            endGamePanel.SetInfo(score);

            yield break;
        }
    }
}