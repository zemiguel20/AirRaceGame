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
        [SerializeField] private TextMeshProUGUI _throttleLabel;

        private RaceController _raceController;

        public void Initialize(RaceController raceController)
        {
            _raceController = raceController;
        }

        // Update is called once per frame
        void Update()
        {
            _chronometerLabel.text = _raceController.Chronometer.Time.ToString("F2");

            _altitudeLabel.text = "A: " + _raceController.Airplane.Position.y.ToString("F0") + " m";

            int kmPerH = Mathf.RoundToInt(_raceController.Airplane.Velocity.magnitude * 3.6f);
            _velocityLabel.text = "V: " + kmPerH + " km/h";

            int throttlePercent = Mathf.RoundToInt(_raceController.Airplane.Throttle * 100);
            _throttleLabel.text = "T: " + throttlePercent + "%";
        }
    }
}
