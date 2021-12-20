using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace AirRace
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private UIDocument mainMenuPanel;
        [SerializeField] private UIDocument mapPanel;

        private void Awake()
        {
            Button playButton = mainMenuPanel.rootVisualElement.Query<Button>("play");
            playButton.clicked += SwitchToMapPanel;

            Button backButton = mapPanel.rootVisualElement.Query<Button>("back");
            backButton.clicked += SwitchToMainPanel;
        }

        private void Start()
        {
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
        }
    }
}