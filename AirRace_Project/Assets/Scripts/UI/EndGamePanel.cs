using TMPro;
using UnityEngine;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    public void SetInfo(int score)
    {
        scoreText.text = score.ToString();
    }
}
