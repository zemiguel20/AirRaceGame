using System.Collections;
using AirRace.Player;
using AirRace.Race;
using AirRace.UI;
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

            operation = SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
            while (operation.isDone == false)
                yield return null;

            SceneManager.UnloadSceneAsync("LoadingScreen");
        }

        private void InitializeScene()
        {
            // Initialize race

            IPlayerInput input = Object.FindObjectOfType<InputController>();

            Airplane player = Object.FindObjectOfType<Airplane>();
            player.Initialize(input);

            Path path = Object.Instantiate(_map.PathPrefab);

            RaceController controller = Object.FindObjectOfType<RaceController>();
            controller.Initialize(player, path, _map.Leaderboard, input);

            // Initialize UI

            CountdownTimerUI countdownTimerUI = Object.FindObjectOfType<CountdownTimerUI>();
            countdownTimerUI.Initialize(controller);

            HUD hud = Object.FindObjectOfType<HUD>();
            hud.Initialize(controller);

            // start race
            controller.StartRace();
        }
    }
}