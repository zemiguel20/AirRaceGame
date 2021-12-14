using System.Collections;
using System.Collections.Generic;
using AirRace.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            AsyncOperation operation;

            if (loadedMap != null)
            {
                operation = SceneManager.UnloadSceneAsync("UI");
                while (operation.isDone == false)
                    yield return null;

                operation = SceneManager.UnloadSceneAsync(loadedMap.SceneName);
                while (operation.isDone == false)
                    yield return null;

                loadedMap = null;
            }

            loadingScreen.SetActive(true);

            operation = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
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
            AsyncOperation operation;

            if (loadedMap != null)
            {
                operation = SceneManager.UnloadSceneAsync("UI");
                while (operation.isDone == false)
                    yield return null;

                operation = SceneManager.UnloadSceneAsync(loadedMap.SceneName);
                while (operation.isDone == false)
                    yield return null;

                loadedMap = null;
            }
            else
            {
                operation = SceneManager.UnloadSceneAsync("MainMenu");
                while (operation.isDone == false)
                    yield return null;
            }


            loadingScreen.SetActive(true);

            operation = SceneManager.LoadSceneAsync(map.SceneName, LoadSceneMode.Additive);
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

            // PlayerInput input = FindObjectOfType<PlayerInput>();

            // AirplaneMovement player = FindObjectOfType<AirplaneMovement>();

            //Path path = Instantiate(loadedMap.PathPrefab);

            RaceController controller = FindObjectOfType<RaceController>();
            //controller.Initialize(player, path, loadedMap.Leaderboard, input);

            // Initialize UI

            HUD hud = FindObjectOfType<HUD>();
            //hud.Initialize(controller);

            EndGamePanel endGamePanel = FindObjectOfType<EndGamePanel>();
            endGamePanel.Initialize(controller, loadedMap.Leaderboard);

            PauseMenu pauseMenu = FindObjectOfType<PauseMenu>();
            pauseMenu.Initialize(controller);

            // events
            // controller.RaceExited += OnRaceExited;
            // controller.RaceRestarted += OnRaceRestart;

            // start race
            //  controller.StartRace();
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

        private void QuitGame()
        {
            Debug.Log("Game Quit!");
            Application.Quit();
        }
    }
}
