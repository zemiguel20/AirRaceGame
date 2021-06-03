using System.Collections;
using UnityEngine;

namespace AirRace.GameManager.States
{
    public class RaceState : State
    {
        //private RaceManager raceManager;
        private Rigidbody player;
       // private UI UI;

        public RaceState(GameManager gameManager) : base(gameManager)
        {
            // this.raceManager = gameManager.raceManager;
            player = gameManager.player;
          //  UI = gameManager.UI;
        }

        public override IEnumerator Start()
        {
            player.isKinematic = false;
            // raceManager.StartRace();
            yield break;
        }

        public override IEnumerator Pause()
        {
            Time.timeScale = 0;
            GameManager.isPaused = true;
            //UI.SetPauseMenuActive(true);
            yield break;
        }

        public override IEnumerator Resume()
        {
            Time.timeScale = 1;
            GameManager.isPaused = false;
           // UI.SetPauseMenuActive(false);
            yield break;
        }
    }
}