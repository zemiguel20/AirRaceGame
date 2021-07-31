using System.Collections;
using System.Collections.Generic;
using AirRace.UI;
using AirRace.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using AirRace.Player;
using AirRace.Race;

namespace AirRace
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private List<MapInfoSO> _maps;
        public List<MapInfoSO> Maps { get => _maps; }

        private MapInfoSO loadedMap;

        void Awake()
        {
            LoadMapsLeaderboards();

            StartCoroutine(LoadMainMenu());
        }

        private void LoadMapsLeaderboards()
        {
            foreach (var mapInfo in _maps)
            {
                string scriptableObjectName = mapInfo.name;
                Leaderboard leaderboard = SaveManager.LoadLeaderboard(scriptableObjectName);
                if (leaderboard != null)
                {
                    mapInfo.Leaderboard.SetTimes(leaderboard.Times);
                }
            }
        }

        private IEnumerator LoadMainMenu()
        {
            if (loadedMap != null)
            {
                SceneManager.UnloadSceneAsync(loadedMap.MapName);
                SceneManager.UnloadSceneAsync("UI");
                loadedMap = null;
            }

            AsyncOperation operation = SceneManager.LoadSceneAsync("LoadingScreen", LoadSceneMode.Additive);
            while (operation.isDone == false)
                yield return null;

            operation = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
            while (operation.isDone == false)
                yield return null;

            SceneManager.UnloadSceneAsync("LoadingScreen");

            InitializeMainMenu();

            yield break;
        }

        private void InitializeMainMenu()
        {
            MainMenu mainMenu = FindObjectOfType<MainMenu>();
            mainMenu.Initialize(_maps);
            mainMenu.MapChosen += OnMapChosen;
            mainMenu.QuitPressed += QuitGame;
        }

        private void OnMapChosen(MapInfoSO map)
        {
            StartCoroutine(LoadMap(map));
        }

        private IEnumerator LoadMap(MapInfoSO map)
        {
            if (loadedMap != null)
            {
                SceneManager.UnloadSceneAsync(loadedMap.MapName);
                SceneManager.UnloadSceneAsync("UI");
                loadedMap = null;
            }
            else
            {
                SceneManager.UnloadSceneAsync("MainMenu");
            }


            AsyncOperation operation = SceneManager.LoadSceneAsync("LoadingScreen", LoadSceneMode.Additive);
            while (operation.isDone == false)
                yield return null;

            operation = SceneManager.LoadSceneAsync(map.SceneName, LoadSceneMode.Additive);
            while (operation.isDone == false)
                yield return null;

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(map.SceneName));

            operation = SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
            while (operation.isDone == false)
                yield return null;

            SceneManager.UnloadSceneAsync("LoadingScreen");

            loadedMap = map;

            InitializeMap();

            yield break;
        }

        private void InitializeMap()
        {
            // Initialize race

            IPlayerInput input = FindObjectOfType<InputController>();

            Airplane player = FindObjectOfType<Airplane>();
            player.Initialize(input);

            Path path = Instantiate(loadedMap.PathPrefab);

            RaceController controller = FindObjectOfType<RaceController>();
            controller.Initialize(player, path, loadedMap.Leaderboard, input);

            // Initialize UI

            CountdownTimerUI countdownTimerUI = FindObjectOfType<CountdownTimerUI>();
            countdownTimerUI.Initialize(controller);

            HUD hud = FindObjectOfType<HUD>();
            hud.Initialize(controller);

            Waypoint waypoint = FindObjectOfType<Waypoint>();
            waypoint.Initialize(player.transform, Camera.main, path);

            EndGamePanel endGamePanel = FindObjectOfType<EndGamePanel>();
            endGamePanel.Initialize(controller, loadedMap.Leaderboard);

            // events
            controller.RaceExited += OnRaceExited;
            controller.RaceRestarted += OnRaceRestart;

            // start race
            controller.StartRace();
        }

        private void OnRaceExited()
        {
            SaveManager.SaveLeaderboard(loadedMap.Leaderboard, loadedMap.name);
            StartCoroutine(LoadMainMenu());
        }

        private void OnRaceRestart()
        {
            StartCoroutine(LoadMap(loadedMap));
        }

        public void QuitGame()
        {
            GameLogger.Debug("Game Quit!");
            Application.Quit();
        }
    }
}
