using UnityEngine;
using TMPro;
using AirRace.Race;

namespace AirRace.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _velocityLabel;
        [SerializeField] private TextMeshProUGUI _altitudeLabel;
        [SerializeField] private TextMeshProUGUI _chronometerLabel;

        private RaceController _raceController;

        public void Initialize(RaceController raceController)
        {
            _raceController = raceController;
        }

        // Update is called once per frame
        void Update()
        {
            _altitudeLabel.text = "Altitude: " + _raceController.Airplane.Position.y.ToString("F0") + " m";

            _chronometerLabel.text = _raceController.Chronometer.Time.ToString("F2");

            float kmPerH = _raceController.Airplane.Velocity.magnitude * 3.6f;
            _velocityLabel.text = Mathf.RoundToInt(kmPerH).ToString() + " km/h";
        }
    }
}
