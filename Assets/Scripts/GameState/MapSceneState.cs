using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AirRace.GameState
{
    public class MapSceneState : SceneState
    {
        private MapInfoSO _map;

        public MapSceneState(GameManager gameManager, MapInfoSO map) : base(gameManager)
        {
            _map = map;
        }

        public override IEnumerator Load()
        {
            yield return LoadScene();
            InitializeScene();
            yield break;
        }

        private IEnumerator LoadScene()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync("LoadingScreen", LoadSceneMode.Additive);
            while (operation.isDone == false)
                yield return null;

            operation = SceneManager.LoadSceneAsync(_map.SceneName, LoadSceneMode.Additive);
            while (operation.isDone == false)
                yield return null;

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(_map.SceneName));

            SceneManager.UnloadSceneAsync("LoadingScreen");
        }

        private void InitializeScene()
        {
            //TODO - intialize map
        }
    }
}