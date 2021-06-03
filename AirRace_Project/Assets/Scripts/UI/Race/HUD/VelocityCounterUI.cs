using UnityEngine;
using TMPro;

namespace AirRace.UI.Race.HUD
{
    public class VelocityCounterUI : MonoBehaviour
    {
        [SerializeField] private Rigidbody _player;
        [SerializeField] private TextMeshProUGUI _label;

        // Update is called once per frame
        void Update()
        {
            float kmPerH = _player.velocity.magnitude * 3600 / 1000;

            _label.text = Mathf.RoundToInt(kmPerH).ToString() + " km/h";
        }
    }
}