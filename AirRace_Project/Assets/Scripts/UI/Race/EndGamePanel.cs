using TMPro;
using UnityEngine;

namespace AirRace.UI.Race
{
    public class EndGamePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        public void SetInfo(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}