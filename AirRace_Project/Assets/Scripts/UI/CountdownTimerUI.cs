using TMPro;
using UnityEngine;

public class CountdownTimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmpText;

    public void SetText(string newText)
    {
        tmpText.text = newText;
    }
}
