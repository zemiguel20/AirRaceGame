using UnityEngine;
using TMPro;

public class VelocityCounterUI : MonoBehaviour
{
    private Rigidbody player;
    private TextMeshProUGUI label;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        label = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        float kmPerH = (player.velocity.magnitude * 3600) / 1000;

        label.text = Mathf.RoundToInt(kmPerH).ToString() + " km/h";
    }
}
