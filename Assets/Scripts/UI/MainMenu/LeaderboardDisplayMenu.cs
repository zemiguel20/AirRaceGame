using System.Text;
using AirRace.Leaderboard;
using TMPro;
using UnityEngine;

namespace AirRace.UI.MainMenu
{
    public class LeaderboardDisplayMenu : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI _leaderboardText;
        [SerializeField] private LeaderboardSO _leaderboard;

        private void OnEnable()
        {
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