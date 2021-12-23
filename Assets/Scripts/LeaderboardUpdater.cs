using System;
using UnityEngine;

namespace AirRace
{
    public class LeaderboardUpdater : MonoBehaviour
    {
        public static event Action<Leaderboard> leaderboardUpdated;

        private SceneLoader sceneLoader;
        private Chronometer chronometer;

        private void Awake()
        {
            sceneLoader = FindObjectOfType<SceneLoader>();
            chronometer = FindObjectOfType<Chronometer>();

            RaceController.raceFinished += UpdateLeaderboard;
        }

        private void UpdateLeaderboard()
        {
            sceneLoader.loadedMap.leaderboard.AddEntry(chronometer.time);
            leaderboardUpdated?.Invoke(sceneLoader.loadedMap.leaderboard);
        }

        private void OnDestroy()
        {
            RaceController.raceFinished -= UpdateLeaderboard;
        }
    }
}
