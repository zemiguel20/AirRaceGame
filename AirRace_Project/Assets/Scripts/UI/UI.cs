using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    private CountdownTimerUI CountdownTimerUI;
    private EndGamePanel EndGamePanel;
    private GameObject PauseMenu;

    private void Awake()
    {
        this.CountdownTimerUI = GetComponentInChildren<CountdownTimerUI>();
        this.EndGamePanel = GetComponentInChildren<EndGamePanel>();
        this.PauseMenu = GameObject.Find("PauseMenu");
    }

    public void SetEndGamePanelActive(bool value)
    {
        this.EndGamePanel.gameObject.SetActive(value);
    }

    public void SetEndGamePanelInfo(int score)
    {
        this.EndGamePanel.SetInfo(score);
    }

    public void SetCountdownTimerActive(bool value)
    {
        this.CountdownTimerUI.gameObject.SetActive(value);
    }

    public void SetCountdownTimerText(string text)
    {
        this.CountdownTimerUI.SetText(text);
    }

}
