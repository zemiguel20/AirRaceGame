using UnityEngine;

namespace AirRace.UI
{
    public class PauseMenu : MonoBehaviour
    {
        private RaceController _raceController;

        public void Initialize(RaceController raceController)
        {
            gameObject.SetActive(false);

            _raceController = raceController;

            //_raceController.GamePaused += OnPause;
            //_raceController.GameResumed += OnResume;
        }

        private void OnPause()
        {
            gameObject.SetActive(true);
        }

        private void OnResume()
        {
            gameObject.SetActive(false);
        }

        public void ResumePressed()
        {
            //_raceController.PauseResumeGame();
        }

        public void RestartPressed()
        {
            // _raceController.RestartRace();
        }

        public void ExitPressed()
        {
            //  _raceController.ExitRace();
        }

    }
}
