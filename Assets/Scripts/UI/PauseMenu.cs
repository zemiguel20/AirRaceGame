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

        private SceneLoader sceneLoader;

        private void Awake()
        {
            pauseController = FindObjectOfType<PauseController>();
            PauseController.pauseStateChanged += OnPauseStateChange;

            //Pause menu should not be active when race ends, as the results panel is shown
            RaceController.raceFinished += DisableMenuActivation;

            resumeButton = document.rootVisualElement.Query<Button>("resume");
            resumeButton.clicked += ResumePressed;

            restartButton = document.rootVisualElement.Query<Button>("restart");
            restartButton.clicked += RestartPressed;

            quitButton = document.rootVisualElement.Query<Button>("quit");
            quitButton.clicked += QuitPressed;

            sceneLoader = FindObjectOfType<SceneLoader>();
        }

        private void Start()
        {
            //Focusing and Unfocusing random element on this Scene fixes the navigation not working
            resumeButton.Focus();
            resumeButton.Blur();

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

        private void ResumePressed()
        {
            pauseController.ResumeGame();
        }

        private void RestartPressed()
        {
            sceneLoader.LoadMap(sceneLoader.loadedMap);
        }

        private void QuitPressed()
        {
            sceneLoader.LoadMainMenu();
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
