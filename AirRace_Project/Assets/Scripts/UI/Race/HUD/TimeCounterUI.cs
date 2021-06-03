using UnityEngine;
using TMPro;
using AirRace.Race;

namespace AirRace.UI.Race.HUD
{
    public class TimeCounterUI : MonoBehaviour
    {
        [SerializeField] private Gradient _colorGradient;
        [SerializeField] private Chronometer _chronometer;
        [SerializeField] private TextMeshProUGUI _label;

        // Update is called once per frame
        void Update()
        {
            _label.text = _chronometer.time.ToString("F2");

            //TODO - set color and image depending on Leaderboard

            //float value = Mathf.Clamp01(raceManager.chronometerTimeValue / raceManager.TIME_LIMIT);
            //label.color = colorGradient.Evaluate(value);
        }
    }
}