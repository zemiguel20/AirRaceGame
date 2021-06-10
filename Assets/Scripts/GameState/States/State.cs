using System.Collections;

namespace AirRace.GameState.States
{
    public abstract class State
    {
        protected GameManager gameManager;

        public State(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public virtual IEnumerator Start()
        {
            yield break;
        }

        public virtual IEnumerator Pause()
        {
            yield break;
        }

        public virtual IEnumerator Resume()
        {
            yield break;
        }

    }
}