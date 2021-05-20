using TMPro;
using UnityEngine;

public class CountdownTimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmpText;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void SetText(string newText)
    {
        tmpText.text = newText;
    }
}
