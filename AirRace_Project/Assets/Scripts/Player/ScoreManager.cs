using AirRace.Core;
using AirRace.Core.SOVariables;
using UnityEngine;
using UnityEngine.Events;

namespace AirRace.Player
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private FloatVariable score;
        [SerializeField] private FloatVariable chronometerTime;
        [SerializeField] private FloatVariable TIME_LIMIT;
        [SerializeField] private UnityEvent ScoreChanged;

        public void UpdateScore()
        {
            score.value += CalculatePoints(chronometerTime.value);
            GameLogger.Debug("Score update: " + score.value);
            ScoreChanged.Invoke();
        }

        private int CalculatePoints(float playerTime)
        {
            float difference = 0;

            if (playerTime < TIME_LIMIT.value)
                difference = TIME_LIMIT.value - playerTime;


            int points = Mathf.RoundToInt(difference * 10);

            return points;
        }


        public void ResetScore()
        {
            GameLogger.Debug("Score Reset");
            score.value = 0;
        }
    }
}