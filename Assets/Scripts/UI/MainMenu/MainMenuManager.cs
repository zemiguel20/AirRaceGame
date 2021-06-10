using AirRace.Core;
using AirRace.Core.SOs;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AirRace.UI.MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private List<LeaderboardSO> _leaderboards;

        private void Awake()
        {
            BootstrapLeaderboards();
        }

        public void Play(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        public void Quit()
        {
            Debug.Log("Quit");
            Application.Quit();
        }



        private void BootstrapLeaderboards()
        {
            foreach (var leaderboard in _leaderboards)
            {
                LeaderboardSerializable serializable = SaveManager.LoadLeaderboard(leaderboard.name);
                leaderboard.SetFromSerializable(serializable);
            }
        }
    }
}