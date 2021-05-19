using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GameState
{
    public class InitialCountdownState : State
    {
        private int initialCountdown;

        private CountdownTimerUI countdownTimerUI;

        public InitialCountdownState(GameManager gameManager) : base(gameManager)
        {
            this.initialCountdown = gameManager.initialCountdown;
            this.countdownTimerUI = gameManager.countdownTimerUI;
        }

        public override IEnumerator Start()
        {
            countdownTimerUI.gameObject.SetActive(true);

            countdownTimerUI.SetText("Starting in...");
            yield return new WaitForSeconds(1.5f);

            for (int i = initialCountdown; i > 0; i--)
            {
                countdownTimerUI.SetText(i.ToString());
                yield return new WaitForSeconds(1);
            }

            countdownTimerUI.SetText("GO");
            gameManager.SetState(new RaceState(gameManager));

            yield return new WaitForSeconds(1);

            countdownTimerUI.gameObject.SetActive(false);
        }
    }
}