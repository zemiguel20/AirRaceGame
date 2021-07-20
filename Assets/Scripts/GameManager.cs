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
        private List<MapInfoSO> _maps;

        private SceneState _sceneState;

        public void ChangeScene(SceneState newScene)
        {
            _sceneState = newScene;
            StartCoroutine(_sceneState.Load());
        }

        void Awake()
        {
            _maps = SaveManager.LoadAllMapInfos();

            ChangeScene(new MainMenuSceneState(this));
        }
    }
}
