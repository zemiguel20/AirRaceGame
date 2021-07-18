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
        public List<MapInfoSO> Maps { get => new List<MapInfoSO>(_maps); }

        // Start is called before the first frame update
        void Start()
        {
            _maps = SaveManager.LoadAllMapInfos();

            SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
        }
    }
}
