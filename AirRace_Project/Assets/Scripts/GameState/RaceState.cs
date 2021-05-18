using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GameState
{
    public class RaceState : State
    {
        private RaceManager raceManager;
        private Rigidbody player;

        public RaceState(GameManager gameManager) : base(gameManager)
        {
            this.raceManager = gameManager.raceManager;
            this.player = gameManager.player;
        }

        public override IEnumerator Start()
        {
            player.isKinematic = false;
            raceManager.StartRace();
            yield break;
        }
    }
}