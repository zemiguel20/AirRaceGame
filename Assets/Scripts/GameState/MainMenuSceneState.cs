using System.Collections;
using AirRace.UI;
using UnityEngine;
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
            SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Additive);

            SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);

            SceneManager.UnloadSceneAsync("LoadingScreen");

            MainMenu mainMenu = Object.FindObjectOfType<MainMenu>();
            mainMenu.Initialize(_gameManager.Maps);

            yield break;
        }


    }
}