using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AirRace.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] GridLayoutGroup grid;
        [SerializeField] MapButton mapButtonPrefab;

        [SerializeField] GameObject menuPanel;
        [SerializeField] GameObject mapSelectionPanel;
        [SerializeField] GameObject mapInfoPanel;


        public void Initialize(List<MapInfoSO> maps)
        {
            LoadMapButtons(maps);
            ShowMenuPanel();
        }

        private void LoadMapButtons(List<MapInfoSO> maps)
        {
            foreach (MapInfoSO map in maps)
            {
                MapButton instance = Instantiate(mapButtonPrefab);
                instance.transform.SetParent(grid.transform);
                instance.Initialize(map);
                instance.MapSelected += ShowMapInfoPanel;
            }
        }

        public void ShowMenuPanel()
        {
            menuPanel.SetActive(true);
            mapSelectionPanel.SetActive(false);
            mapInfoPanel.SetActive(false);
        }

        public void ShowMapSelectionPanel()
        {
            menuPanel.SetActive(false);
            mapSelectionPanel.SetActive(true);
            mapInfoPanel.SetActive(false);
        }

        public void ShowMapInfoPanel(MapInfoSO map)
        {
            menuPanel.SetActive(false);
            mapSelectionPanel.SetActive(false);
            mapInfoPanel.SetActive(true);
        }

    }
}