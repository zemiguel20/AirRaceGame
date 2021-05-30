using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GameState
{
    public class InitialCountdownState : State
    {
        private int initialCountdown;

        private UI UI;

        public InitialCountdownState(GameManager gameManager) : base(gameManager)
        {
            this.initialCountdown = gameManager.initialCountdown;
            this.UI = gameManager.UI;
        }

        public override IEnumerator Start()
        {
            UI.SetCountdownTimerActive(true);

            UI.SetCountdownTimerText("Starting in...");
            yield return new WaitForSeconds(1.5f);

            for (int i = initialCountdown; i > 0; i--)
            {
                UI.SetCountdownTimerText(i.ToString());
                yield return new WaitForSeconds(1);
            }

            UI.SetCountdownTimerText("GO");
            gameManager.SetState(new RaceState(gameManager));

            yield return new WaitForSeconds(1);

            UI.SetCountdownTimerActive(false);
        }
    }
}