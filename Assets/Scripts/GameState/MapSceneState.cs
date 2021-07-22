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
            AsyncOperation operation = SceneManager.LoadSceneAsync("LoadingScreen", LoadSceneMode.Additive);
            while (operation.isDone == false)
                yield return null;

            //TODO - Load Map

            Debug.Log("Map Loaded: " + _map.MapName);
            yield break;
        }
    }
}