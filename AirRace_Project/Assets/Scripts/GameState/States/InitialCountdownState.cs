using AirRace.Core;
using System.Collections;
using UnityEngine;

namespace AirRace.GameState.States
{
    public class InitialCountdownState : State
    {
        private int initialCountdown = 3;

        // UI ui;

        public InitialCountdownState(GameManager gameManager) : base(gameManager)
        {
        }

        public override IEnumerator Start()
        {
            //UI.SetCountdownTimerActive(true);

            //UI.SetCountdownTimerText("Starting in...");
            yield return new WaitForSeconds(1.5f);

            for (int i = initialCountdown; i > 0; i--)
            {
                GameLogger.Debug(i.ToString());

                //UI.SetCountdownTimerText(i.ToString());
                yield return new WaitForSeconds(1);
            }

            GameLogger.Debug("GO");

            //UI.SetCountdownTimerText("GO");
            gameManager.SetState(new RaceState(gameManager));

            yield return new WaitForSeconds(1);

            //UI.SetCountdownTimerActive(false);
        }
    }
}