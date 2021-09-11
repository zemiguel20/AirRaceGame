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

        private Map _map;

        public event Action<Map> MapSelected;


        public void Initialize(Map map)
        {
            _nameLabel.text = map.Name;
            _image.sprite = map.Image;

            _map = map;
        }

        public void OnButtonClicked()
        {
            MapSelected.Invoke(_map);
        }
    }
}
