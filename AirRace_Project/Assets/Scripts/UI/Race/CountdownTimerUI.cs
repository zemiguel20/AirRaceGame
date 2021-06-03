using TMPro;
using UnityEngine;

namespace AirRace.UI.Race
{
    public class CountdownTimerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tmpText;

        public void SetText(string newText)
        {
            tmpText.text = newText;
        }
    }
}