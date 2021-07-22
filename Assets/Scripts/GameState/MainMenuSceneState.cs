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
            AsyncOperation operation = SceneManager.LoadSceneAsync("LoadingScreen", LoadSceneMode.Additive);
            while (operation.isDone == false)
                yield return null;

            operation = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
            while (operation.isDone == false)
                yield return null;

            SceneManager.UnloadSceneAsync("LoadingScreen");

            MainMenu mainMenu = Object.FindObjectOfType<MainMenu>();
            mainMenu.Initialize(_gameManager.Maps);
            mainMenu.MapChosen += _gameManager.LoadMap;
            mainMenu.QuitPressed += _gameManager.QuitGame;

            yield break;
        }

        public override IEnumerator LoadMap(MapInfoSO map)
        {
            SceneManager.UnloadSceneAsync("MainMenu");
            _gameManager.ChangeScene(new MapSceneState(_gameManager, map));
            yield break;
        }


    }
}