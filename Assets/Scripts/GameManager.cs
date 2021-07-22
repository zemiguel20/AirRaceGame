using System.Collections;
using System.Collections.Generic;
using AirRace.UI;
using AirRace.Utils;
using AirRace.GameState;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AirRace
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private List<MapInfoSO> _maps;
        public List<MapInfoSO> Maps { get => _maps; }

        private SceneState _sceneState;

        public void ChangeScene(SceneState newScene)
        {
            _sceneState = newScene;
            StartCoroutine(_sceneState.Load());
        }

        void Awake()
        {
            LoadMapsLeaderboards();

            ChangeScene(new MainMenuSceneState(this));
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

        public void LoadMap(MapInfoSO map)
        {
            StartCoroutine(_sceneState.LoadMap(map));
        }

        public void QuitGame()
        {
            GameLogger.Debug("Game Quit!");
            Application.Quit();
        }
    }
}
