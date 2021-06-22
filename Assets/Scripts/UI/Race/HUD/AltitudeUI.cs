using UnityEngine;
using TMPro;

namespace AirRace.UI.Race.HUD
{
    public class AltitudeUI : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private TextMeshProUGUI _label;

        // Update is called once per frame
        void Update()
        {
            _label.text = "Altitude: " + _player.position.y.ToString("F0") + " m";
        }
    }
}
