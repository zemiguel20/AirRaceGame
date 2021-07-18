using System.Collections;
using System.Collections.Generic;
using AirRace.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AirRace
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            // Keeping one instance only
            if (Instance != null && Instance != this)
            {
                GameLogger.Debug("GameManager already exists, removing this object.");
                Destroy(GetComponent<GameManager>());
            }
            else
            {
                Instance = this;
            }
        }
        #endregion

        private List<MapInfoSO> _maps;
        public List<MapInfoSO> Maps { get => new List<MapInfoSO>(_maps); private set => _maps = value; }

        // Start is called before the first frame update
        void Start()
        {
            Bootstrap();
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
            loadOperation.allowSceneActivation = true;
        }

        private void Bootstrap()
        {
            _maps = new List<MapInfoSO>();

            var maps = Resources.LoadAll<MapInfoSO>("MapInfo");
            foreach (var map in maps)
            {
                _maps.Add(map);
            }

            // TODO - load leaderboards
        }
    }
}
