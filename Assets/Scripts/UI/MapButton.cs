using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace AirRace.UI
{
    public class MapButton : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _nameLabel;

        private MapInfoSO _map;

        public event Action<MapInfoSO> MapSelected;


        public void Initialize(MapInfoSO mapInfo)
        {
            _nameLabel.text = mapInfo.MapName;
            _image.sprite = mapInfo.Image;

            _map = mapInfo;
        }

        public void OnButtonClicked()
        {
            MapSelected.Invoke(_map);
        }
    }
}
