using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameState
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