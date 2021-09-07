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
        [SerializeField] GameObject loadingScreen;

        [SerializeField] private List<Map> _maps;
        public List<Map> Maps { get => _maps; }

        private Map loadedMap;

        void Awake()
        {
            LoadMapsLeaderboards();

            StartCoroutine(LoadMainMenu());
        }

        private void LoadMapsLeaderboards()
        {
            foreach (var map in _maps)
            {
                List<float> values = SaveManager.LoadLeaderboard(map.Name);
                if (values != null)
                {
                    map.Leaderboard.SetTimes(values);
                }
            }
        }

        private IEnumerator LoadMainMenu()
        {
            if (loadedMap != null)
            {
                SceneManager.UnloadSceneAsync(loadedMap.SceneName);
                SceneManager.UnloadSceneAsync("UI");
                loadedMap = null;
            }

            loadingScreen.SetActive(true);

            AsyncOperation operation = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
            while (operation.isDone == false)
                yield return null;

            InitializeMainMenu();

            loadingScreen.SetActive(false);


            yield break;
        }

        private void InitializeMainMenu()
        {
            MainMenu mainMenu = FindObjectOfType<MainMenu>();
            mainMenu.Initialize(_maps);
            mainMenu.MapChosen += OnMapChosen;
            mainMenu.QuitPressed += QuitGame;
        }

        private void OnMapChosen(Map map)
        {
            StartCoroutine(LoadMap(map));
        }

        private IEnumerator LoadMap(Map map)
        {
            if (loadedMap != null)
            {
                SceneManager.UnloadSceneAsync(loadedMap.SceneName);
                SceneManager.UnloadSceneAsync("UI");
                loadedMap = null;
            }
            else
            {
                SceneManager.UnloadSceneAsync("MainMenu");
            }


            loadingScreen.SetActive(true);

            AsyncOperation operation = SceneManager.LoadSceneAsync(map.SceneName, LoadSceneMode.Additive);
            while (operation.isDone == false)
                yield return null;

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(map.SceneName));

            operation = SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
            while (operation.isDone == false)
                yield return null;


            loadedMap = map;

            InitializeMap();

            loadingScreen.SetActive(false);

            yield break;
        }

        private void InitializeMap()
        {
            // Initialize race

            PlayerInput input = FindObjectOfType<PlayerInput>();

            Airplane player = FindObjectOfType<Airplane>();

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

            PauseMenu pauseMenu = FindObjectOfType<PauseMenu>();
            pauseMenu.Initialize(controller);

            // events
            controller.RaceExited += OnRaceExited;
            controller.RaceRestarted += OnRaceRestart;

            // start race
            controller.StartRace();
        }

        private void OnRaceExited()
        {
            SaveManager.SaveLeaderboard(loadedMap.Leaderboard.Times, loadedMap.Name);
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
