using Assets.Scripts.GameLogger;
using UnityEngine;
using UnityEngine.Events;

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
        float pointsTemp = 0;

        if (playerTime < TIME_LIMIT.value)
            pointsTemp = TIME_LIMIT.value - playerTime;


        int points = Mathf.RoundToInt(pointsTemp * 10);

        return points;
    }


    public void ResetScore()
    {
        GameLogger.Debug("Score Reset");
        score.value = 0;
    }
}
