using System.Collections;
using System.Collections.Generic;
using AirRace.Race;
using UnityEngine;

namespace AirRace.UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private RaceController _gameManager;

        private void Start()
        {
            gameObject.SetActive(false);

            _gameManager.GamePaused += OnPause;
            _gameManager.GameResumed += OnResume;
        }

        private void OnDestroy()
        {
            _gameManager.GamePaused -= OnPause;
            _gameManager.GameResumed -= OnResume;
        }

        private void OnPause()
        {
            gameObject.SetActive(true);
        }

        private void OnResume()
        {
            gameObject.SetActive(false);
        }


    }
}
