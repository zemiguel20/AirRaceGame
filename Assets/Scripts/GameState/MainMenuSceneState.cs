using System.Collections;
using UnityEngine.SceneManagement;

namespace AirRace.GameState
{
    public class MainMenuSceneState : SceneState
    {
        public MainMenuSceneState(GameManager gameManager) : base(gameManager)
        {
        }

        public override IEnumerator Load()
        {
            SceneManager.LoadSceneAsync("LoadingScreen", LoadSceneMode.Additive);

            // TODO - Load Main Menu

            yield break;
        }


    }
}