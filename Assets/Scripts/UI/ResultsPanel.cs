using UnityEngine;
using UnityEngine.UIElements;

namespace AirRace
{
    public class ResultsPanel : MonoBehaviour
    {
        [SerializeField] private UIDocument document;
        private Label playerTimeLabel;
        private Label leaderboardLabel;
        private Button quitButton;
        private Button restartButton;

        private Chronometer chronometer;

        private SceneLoader sceneLoader;

        private void Awake()
        {
            chronometer = FindObjectOfType<Chronometer>();

            playerTimeLabel = document.rootVisualElement.Query<Label>("player-time");
            leaderboardLabel = document.rootVisualElement.Query<Label>("leaderboard");

            quitButton = document.rootVisualElement.Query<Button>("quit");
            quitButton.clicked += QuitPressed;

            restartButton = document.rootVisualElement.Query<Button>("restart");
            restartButton.clicked += RestartPressed;

            RaceController.raceFinished += ShowPanel;
            LeaderboardUpdater.leaderboardUpdated += UpdatePanelInfo;

            sceneLoader = FindObjectOfType<SceneLoader>();
        }

        private void Start()
        {
            //Only becomes visible at race end
            document.rootVisualElement.visible = false;
        }

        private void RestartPressed()
        {
            sceneLoader.LoadMap(sceneLoader.loadedMap);
        }

        private void QuitPressed()
        {
            sceneLoader.LoadMainMenu();
        }

        private void ShowPanel()
        {
            document.rootVisualElement.visible = true;
        }

        private void UpdatePanelInfo(Leaderboard leaderboard)
        {
            playerTimeLabel.text = chronometer.time.ToString("F2");

            leaderboardLabel.text = "";
            for (int i = 0; i < leaderboard.times.Count; i++)
            {
                leaderboardLabel.text += (i + 1) + ": " + leaderboard.times[i].ToString("F2") + "\n";
            }
        }

        private void OnDestroy()
        {
            //Unsub from events on destroy
            RaceController.raceFinished -= ShowPanel;
            LeaderboardUpdater.leaderboardUpdated -= UpdatePanelInfo;
        }
    }
}