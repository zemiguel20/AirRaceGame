using UnityEngine;
using UnityEngine.UIElements;

namespace AirRace
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private UIDocument mainMenuPanel;
        private Button menuPlayButton;
        private Button menuQuitButton;

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
                        leaderboardLabel.text += i + ": " + selectedMap.leaderboard.times[i] + "\n";
                    }
                }
                else
                {
                    leaderboardLabel.text = "(No player times)";
                }
            }
            else
            {
                leaderboardLabel.text = "(No leaderboard selected)";
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