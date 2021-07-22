using System.Text;
using TMPro;
using UnityEngine;

namespace AirRace.UI
{
    public class LeaderboardUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _leaderboardText;

        public void Initialize(Leaderboard leaderboard)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < Leaderboard.SIZE; i++)
            {
                try
                {
                    float value = leaderboard.Times[i];
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