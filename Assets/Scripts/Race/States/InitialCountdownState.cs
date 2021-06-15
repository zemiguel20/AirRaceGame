using AirRace.Core;
using AirRace.UI.Race;
using System.Collections;
using UnityEngine;

namespace AirRace.Race.States
{
    public class InitialCountdownState : State
    {
        private int initialCountdown = 3;

        private UIManager _UI;

        public InitialCountdownState(GameManager gameManager) : base(gameManager)
        {
            _UI = gameManager.GetUIManager();
        }

        public override IEnumerator Start()
        {
            _UI.SetCountdownTimerActive(true);

            _UI.SetCountdownTimerText("Starting in...");
            yield return new WaitForSeconds(1.5f);

            for (int i = initialCountdown; i > 0; i--)
            {
                GameLogger.Debug(i.ToString());

                _UI.SetCountdownTimerText(i.ToString());
                yield return new WaitForSeconds(1);
            }

            GameLogger.Debug("GO");

            _UI.SetCountdownTimerText("GO");
            gameManager.SetState(new RaceState(gameManager));

            yield return new WaitForSeconds(1);

            _UI.SetCountdownTimerActive(false);
        }
    }
}