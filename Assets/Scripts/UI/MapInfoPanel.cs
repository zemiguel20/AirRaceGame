using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace AirRace.UI
{
    public class MapInfoPanel : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _nameLabel;
        [SerializeField] private LeaderboardUI _leaderboardUI;

        private Map _selectedMap;

        public event Action<Map> PlayMapPressed;

        public void Initialize(Map map)
        {
            _selectedMap = map;

            _image.sprite = _selectedMap.Image;
            _nameLabel.text = _selectedMap.Name;
            _leaderboardUI.Initialize(map.Leaderboard);
        }

        public void OnPlayMapPressed()
        {
            PlayMapPressed?.Invoke(_selectedMap);
        }
    }
}
