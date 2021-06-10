using AirRace.Core.SOs;
using System.Text;
using TMPro;
using UnityEngine;

namespace AirRace.UI.Race
{
    public class EndGamePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private TextMeshProUGUI _leaderboardText;
        [SerializeField] private LeaderboardSO _leaderboard;

        public void SetInfo(float playerTime)
        {
            _timeText.text = playerTime.ToString("F2");

            StringBuilder sb = new StringBuilder();


            for (int i = 0; i < _leaderboard.Size(); i++)
            {
                try
                {
                    float value = _leaderboard.Values()[i];
                    sb.AppendFormat("{0,-5} {1,-10} {2} \n", (i + 1).ToString(), ".........", value.ToString("F2"));
                }
                catch (System.Exception)
                {
                    sb.AppendFormat("{0,-5} {1,-10} {2} \n", (i + 1).ToString(), ".........", "N/A");
                }
            }

            _leaderboardText.text = sb.ToString();

        }
    }
}