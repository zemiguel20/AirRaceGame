using System.Collections;
using System.Collections.Generic;
using AirRace.Core;
using UnityEngine;

namespace AirRace.Race
{
    public class Timer
    {
        public delegate void TimerEndHandler();
        public event TimerEndHandler TimerEnded;


        public int RemainingSeconds { get; private set; }

        private bool _isRunning = false;
        public bool IsRunning { get => _isRunning; }

        public IEnumerator StartTimer(int time)
        {
            if (_isRunning)
            {
                GameLogger.Debug("Timer already running.");
                yield break;
            }
            else
            {
                _isRunning = true;
                RemainingSeconds = time;

                GameLogger.Debug(RemainingSeconds.ToString());

                while (RemainingSeconds > 0)
                {
                    yield return new WaitForSeconds(1);
                    RemainingSeconds--;

                    GameLogger.Debug(RemainingSeconds.ToString());
                }

                _isRunning = false;

                TimerEnded.Invoke();
            }
        }
    }
}
