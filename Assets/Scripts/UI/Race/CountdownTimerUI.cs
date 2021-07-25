using System.Collections;
using AirRace.Race;
using TMPro;
using UnityEngine;

namespace AirRace.UI.Race
{
    public class CountdownTimerUI : MonoBehaviour
    {
        [SerializeField] private RaceController _gameManager;

        private TextMeshProUGUI tmpText;

        private void Start()
        {
            tmpText = GetComponent<TextMeshProUGUI>();

            tmpText.text = "Starting in...";

            //_gameManager.Timer.TimerEnded += OnTimerEnded;
        }

        private void OnDisable()
        {
            //_gameManager.Timer.TimerEnded -= OnTimerEnded;
        }

        private void Update()
        {
            // if (_gameManager.Timer.IsRunning)
            // {
            //     tmpText.text = _gameManager.Timer.RemainingSeconds.ToString();
            // }
        }

        private void OnTimerEnded()
        {
            StartCoroutine(DisplayFinalTextAndDisable());
        }

        private IEnumerator DisplayFinalTextAndDisable()
        {
            tmpText.text = "GO";
            yield return new WaitForSeconds(1.5f);

            gameObject.SetActive(false);
        }

    }
}