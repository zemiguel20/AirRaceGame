using System.Collections;

namespace AirRace.GameState
{
    public abstract class SceneState
    {
        protected GameManager _gameManager;

        public SceneState(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public virtual IEnumerator Load()
        {
            yield break;
        }

        public virtual IEnumerator LoadMap(MapInfoSO map)
        {
            yield break;
        }
    }
}