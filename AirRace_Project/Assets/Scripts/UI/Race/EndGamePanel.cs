using TMPro;
using UnityEngine;

namespace AirRace.UI.Race
{
    public class EndGamePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        public void SetInfo(float playerTime)
        {
            scoreText.text = playerTime.ToString("F2");
        }
    }
}