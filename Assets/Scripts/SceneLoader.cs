using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AirRace
{
    public class SceneLoader : MonoBehaviour
    {
        private Scene currentScene;

        public Map loadedMap { get; private set; }

        private void Awake()
        {
            LoadMainMenu();
        }

        public void LoadMainMenu()
        {
            loadedMap = null;
            StartCoroutine(LoadScene("MainMenu"));
        }

        public void LoadMap(Map map)
        {
            loadedMap = map;
            StartCoroutine(LoadScene(map.name));
        }

        private IEnumerator LoadScene(string name)
        {
            //Load Loading screen
            AsyncOperation operation = SceneManager.LoadSceneAsync("LoadingScreen", LoadSceneMode.Additive);
            yield return new WaitUntil(() => operation.isDone);

            LoadingScreenController loadingScreenController = FindObjectOfType<LoadingScreenController>();

            //Unload current scene
            if (currentScene.buildIndex != -1)
            {
                AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(currentScene);
                yield return new WaitUntil(() => unloadOperation.isDone);
            }

            //Load new scene
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
            loadOperation.allowSceneActivation = true;

            loadingScreenController.Initialize(loadOperation);

            yield return new WaitUntil(() => loadOperation.isDone);

            currentScene = SceneManager.GetSceneByName(name);

            //Unload Loading screen
            SceneManager.UnloadSceneAsync("LoadingScreen");
        }
    }
}
