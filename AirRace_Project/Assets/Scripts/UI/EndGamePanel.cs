using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void SetInfo(int score)
    {
        scoreText.text = score.ToString();
    }
}
