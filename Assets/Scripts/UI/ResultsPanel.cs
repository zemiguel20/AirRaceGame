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
        }

        private void Start()
        {
            //Only becomes visible at race end
            document.rootVisualElement.visible = false;
        }

        private void RestartPressed()
        {
            //TODO:
        }

        private void QuitPressed()
        {
            //TODO:
        }

        private void ShowPanel()
        {
            playerTimeLabel.text = chronometer.time.ToString("F2");
            //TODO: set leaderboard text
            document.rootVisualElement.visible = true;
        }

        private void OnDestroy()
        {
            //Unsub from events on destroy
            RaceController.raceFinished -= ShowPanel;
        }
    }
}