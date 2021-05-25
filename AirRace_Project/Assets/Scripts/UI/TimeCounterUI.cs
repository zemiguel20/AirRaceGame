using UnityEngine;
using TMPro;

public class TimeCounterUI : MonoBehaviour
{
    [SerializeField] private Gradient colorGradient;

    private TextMeshProUGUI label;
    private RaceManager raceManager;

    private void Awake()
    {
        label = GetComponent<TextMeshProUGUI>();
        raceManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<RaceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        label.text = raceManager.timeCounter.ToString("F1");
        float value = Mathf.Clamp01(raceManager.timeCounter / raceManager.TIME_LIMIT);
        label.color = colorGradient.Evaluate(value);
    }
}
