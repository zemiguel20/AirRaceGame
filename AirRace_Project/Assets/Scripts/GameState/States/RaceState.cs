using AirRace.Core;
using AirRace.Core.Events;
using System.Collections;
using UnityEngine;

namespace AirRace.GameState.States
{
    public class RaceState : State
    {
        private EventManager _eventManager;
        // private UI UI;

        public RaceState(GameManager gameManager) : base(gameManager)
        {
            _eventManager = gameManager.GetEventManager();
            //  UI = gameManager.UI;
        }

        public override IEnumerator Start()
        {
            GameLogger.Debug("Race Started");

            _eventManager.RaiseRaceStartedEvent();
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