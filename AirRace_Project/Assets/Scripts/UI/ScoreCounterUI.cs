using UnityEngine;
using TMPro;


public class ScoreCounterUI : MonoBehaviour
{
    private TextMeshProUGUI label;
    private RaceManager raceManager;

    private void Awake()
    {
        label = GetComponent<TextMeshProUGUI>();
        raceManager = GameObject.Find("RaceManager").GetComponent<RaceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        label.text = raceManager.score.ToString();
    }
}
