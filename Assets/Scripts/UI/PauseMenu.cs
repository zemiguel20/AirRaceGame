using UnityEngine;
using UnityEngine.UIElements;

namespace AirRace
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private UIDocument document;
        private Button resumeButton;
        private Button restartButton;
        private Button quitButton;

        private PauseController pauseController;

        private void Awake()
        {
            pauseController = FindObjectOfType<PauseController>();
            PauseController.pauseStateChanged += OnPauseStateChange;

            //Pause menu should not be active when race ends, as the results panel is shown
            RaceController.raceFinished += DisableMenuActivation;

            resumeButton = document.rootVisualElement.Query<Button>("resume");
            resumeButton.RegisterCallback<MouseOverEvent, Color>(SetBackgroundColor, Color.red);
            resumeButton.RegisterCallback<MouseOutEvent, Color>(SetBackgroundColor, Color.white);
            resumeButton.RegisterCallback<ClickEvent>(ResumePressed);

            restartButton = document.rootVisualElement.Query<Button>("restart");
            restartButton.RegisterCallback<MouseOverEvent, Color>(SetBackgroundColor, Color.red);
            restartButton.RegisterCallback<MouseOutEvent, Color>(SetBackgroundColor, Color.white);
            restartButton.RegisterCallback<ClickEvent>(RestartPressed);

            quitButton = document.rootVisualElement.Query<Button>("quit");
            quitButton.RegisterCallback<MouseOverEvent, Color>(SetBackgroundColor, Color.red);
            quitButton.RegisterCallback<MouseOutEvent, Color>(SetBackgroundColor, Color.white);
            quitButton.RegisterCallback<ClickEvent>(QuitPressed);
        }

        private void Start()
        {
            //Game running by default
            document.rootVisualElement.visible = false;
        }

        private void OnPauseStateChange(bool paused)
        {
            document.rootVisualElement.visible = paused;
        }

        private void DisableMenuActivation()
        {
            PauseController.pauseStateChanged -= OnPauseStateChange;
        }

        private void SetBackgroundColor(EventBase evt, Color color)
        {
            (evt.currentTarget as Button).style.backgroundColor = color;
        }

        public void ResumePressed(ClickEvent evt)
        {
            pauseController.ResumeGame();
        }

        public void RestartPressed(ClickEvent evt)
        {
            //TODO: reload current map
        }

        public void QuitPressed(ClickEvent evt)
        {
            //TODO: quit to main menu
        }


        //Cleanup on object destroy
        private void OnDestroy()
        {
            //Unsub from events
            PauseController.pauseStateChanged -= OnPauseStateChange;
            RaceController.raceFinished -= DisableMenuActivation;
        }

    }
}
