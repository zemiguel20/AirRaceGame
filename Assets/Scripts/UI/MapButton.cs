using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace AirRace.UI
{
    public class MapButton : MonoBehaviour
    {
        [SerializeField] Image _image;
        [SerializeField] TextMeshProUGUI _nameLabel;

        public void SetMapInfo(MapInfoSO mapInfo)
        {
            _nameLabel.text = mapInfo.MapName;
            _image.sprite = mapInfo.Image;
        }
    }
}
