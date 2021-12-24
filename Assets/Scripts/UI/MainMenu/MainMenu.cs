using UnityEngine;
using UnityEngine.UIElements;

namespace AirRace
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private UIDocument mainMenuPanel;
        private Button menuPlayButton;
        private Button menuQuitButton;
        private Label versionLabel;

        [SerializeField] private UIDocument mapPanel;
        private Button mapPanelBackButton;
        private Button mapPanelPlayButton;

        [SerializeField] private VisualTreeAsset mapEntryTemplate;
        private Label leaderboardLabel;

        private MapListViewController mapListController;

        private SceneLoader sceneLoader;

        private void Awake()
        {
            //MainMenuPanel
            menuPlayButton = mainMenuPanel.rootVisualElement.Query<Button>("play");
            menuQuitButton = mainMenuPanel.rootVisualElement.Query<Button>("quit");

            versionLabel = mainMenuPanel.rootVisualElement.Query<Label>("version");

            //MapPanel
            mapPanelBackButton = mapPanel.rootVisualElement.Query<Button>("back");
            mapPanelPlayButton = mapPanel.rootVisualElement.Query<Button>("play");
            leaderboardLabel = mapPanel.rootVisualElement.Query<Label>("leaderboard");

            MapRepository mapRepository = FindObjectOfType<MapRepository>();
            ListView mapListView = mapPanel.rootVisualElement.Query<ListView>("map-list");
            mapListController = new MapListViewController(mapListView, mapRepository, mapEntryTemplate);

            sceneLoader = FindObjectOfType<SceneLoader>();
        }

        private void Start()
        {
            menuPlayButton.clicked += SwitchToMapPanel;
            menuQuitButton.clicked += QuitGame;

            mapPanelBackButton.clicked += SwitchToMainPanel;
            mapPanelPlayButton.clicked += LoadSelectedMap;

            mapListController.selectedMapChanged += UpdateMapPanel;
            mapListController.InitializeMapList();

            versionLabel.text = "Version " + Application.version;

            //Focusing and Unfocusing random element on this Scene fixes the navigation not working
            menuPlayButton.Focus();
            menuPlayButton.Blur();

            SwitchToMainPanel();
        }

        private void SwitchToMainPanel()
        {
            mainMenuPanel.rootVisualElement.visible = true;
            mapPanel.rootVisualElement.visible = false;
        }

        private void SwitchToMapPanel()
        {
            mainMenuPanel.rootVisualElement.visible = false;
            mapPanel.rootVisualElement.visible = true;
            UpdateMapPanel();
        }

        private void UpdateMapPanel()
        {
            mapPanelPlayButton.SetEnabled(mapListController.selectedMap != null);

            Map selectedMap = mapListController.selectedMap;
            leaderboardLabel.text = "";
            if (selectedMap != null)
            {
                if (selectedMap.leaderboard.times.Count > 0)
                {
                    for (int i = 0; i < selectedMap.leaderboard.times.Count; i++)
                    {
                        leaderboardLabel.text += (i + 1) + ": " + selectedMap.leaderboard.times[i].ToString("F2") + "\n";
                    }
                }
                else
                {
                    leaderboardLabel.text = "(No player times)";
                }
            }
            else
            {
                leaderboardLabel.text = "(No map selected)";
            }

        }

        private void LoadSelectedMap()
        {
            sceneLoader.LoadMap(mapListController.selectedMap);
        }

        private void QuitGame()
        {
            Application.Quit();
        }
    }
}