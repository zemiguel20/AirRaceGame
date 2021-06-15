using AirRace.Race;
using UnityEngine;

namespace AirRace.UI.Race
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private EndGamePanel EndGamePanel;
        [SerializeField] private GameObject PauseMenu;

        public void SetEndGamePanelActive(bool value)
        {
            EndGamePanel.gameObject.SetActive(value);
        }

        public void SetEndGamePanelInfo(float playerTime)
        {
            EndGamePanel.SetInfo(playerTime);
        }

        public void SetPauseMenuActive(bool value)
        {
            PauseMenu.SetActive(value);
        }

    }
}