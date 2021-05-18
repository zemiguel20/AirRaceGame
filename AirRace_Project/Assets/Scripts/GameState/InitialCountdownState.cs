using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GameState
{
    public class InitialCountdownState : State
    {
        private int initialCountdown;

        public InitialCountdownState(GameManager gameManager, int initialCountdown) : base(gameManager)
        {
            this.initialCountdown = initialCountdown;
        }

        public override IEnumerator Start()
        {
            //TODO - replace prints with UI

            Debug.Log("Starting in...");
            yield return new WaitForSeconds(1.5f);

            for (int i = initialCountdown; i > 0; i--)
            {
                Debug.Log(i);
                yield return new WaitForSeconds(1);
            }

            Debug.Log("Started");
            gameManager.SetState(new RaceState(gameManager));
        }
    }
}