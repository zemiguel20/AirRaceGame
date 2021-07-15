using System.Collections;
using System.Collections.Generic;
using AirRace.Utils;
using UnityEngine;

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

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
