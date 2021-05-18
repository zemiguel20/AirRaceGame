using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GameState
{
    public class EndGameState : State
    {
        private float score;
        private Rigidbody player;

        public EndGameState(GameManager gameManager) : base(gameManager)
        {
            score = gameManager.raceManager.score;
            player = gameManager.player;
        }

        public override IEnumerator Start()
        {
            player.isKinematic = true;

            //TODO - replace with UI
            Debug.Log("Final Score: " + score);

            yield break;
        }
    }
}