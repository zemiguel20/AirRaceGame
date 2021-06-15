using AirRace.Core;
using AirRace.Core.Events;
using AirRace.UI.Race;
using System.Collections;
using UnityEngine;

namespace AirRace.Race.States
{
    public class RaceState : State
    {
        private EventManager _eventManager;
        private UIManager _UI;

        public RaceState(GameManager gameManager) : base(gameManager)
        {
            _eventManager = gameManager.GetEventManager();
            _UI = gameManager.GetUIManager();
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
            _UI.SetPauseMenuActive(true);
            yield break;
        }

        public override IEnumerator Resume()
        {
            Time.timeScale = 1;
            GameManager.isPaused = false;
            _UI.SetPauseMenuActive(false);
            yield break;
        }
    }
}