using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AirRace.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup grid;
        [SerializeField] private MapButton mapButtonPrefab;

        [SerializeField] private GameObject menuPanel;
        [SerializeField] private GameObject mapSelectionPanel;
        [SerializeField] private MapInfoPanel mapInfoPanel;

        public event Action<Map> MapChosen;
        public event Action QuitPressed;

        public void Initialize(List<Map> maps)
        {
            LoadMapButtons(maps);
            mapInfoPanel.PlayMapPressed += OnPlayMapPressed;
            ShowMenuPanel();
        }

        private void LoadMapButtons(List<Map> maps)
        {
            foreach (Map map in maps)
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
            mapInfoPanel.gameObject.SetActive(false);
        }

        public void ShowMapSelectionPanel()
        {
            menuPanel.SetActive(false);
            mapSelectionPanel.SetActive(true);
            mapInfoPanel.gameObject.SetActive(false);
        }

        public void ShowMapInfoPanel(Map map)
        {
            menuPanel.SetActive(false);
            mapSelectionPanel.SetActive(false);
            mapInfoPanel.gameObject.SetActive(true);

            mapInfoPanel.Initialize(map);
        }

        public void OnPlayMapPressed(Map map)
        {
            MapChosen?.Invoke(map);
        }

        public void OnQuitGamePressed(){
            QuitPressed?.Invoke();
        }

    }
}