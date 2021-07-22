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

        private MapInfoSO _selectedMap;

        public event Action<MapInfoSO> PlayMapPressed;

        public void Initialize(MapInfoSO map)
        {
            _selectedMap = map;

            _image.sprite = _selectedMap.Image;
            _nameLabel.text = _selectedMap.MapName;
            _leaderboardUI.Initialize(map.Leaderboard);
        }

        public void OnPlayMapPressed()
        {
            PlayMapPressed?.Invoke(_selectedMap);
        }
    }
}
