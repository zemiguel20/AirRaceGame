using UnityEngine;

namespace AirRace.UI.Race
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private CountdownTimerUI CountdownTimerUI;
        [SerializeField] private EndGamePanel EndGamePanel;
        [SerializeField] private GameObject PauseMenu;

        public void SetEndGamePanelActive(bool value)
        {
            EndGamePanel.gameObject.SetActive(value);
        }

        public void SetEndGamePanelInfo(int score)
        {
            EndGamePanel.SetInfo(score);
        }

        public void SetCountdownTimerActive(bool value)
        {
            CountdownTimerUI.gameObject.SetActive(value);
        }

        public void SetCountdownTimerText(string text)
        {
            CountdownTimerUI.SetText(text);
        }

        public void SetPauseMenuActive(bool value)
        {
            PauseMenu.SetActive(value);
        }

    }
}