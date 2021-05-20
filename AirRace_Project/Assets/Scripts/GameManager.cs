using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameState;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int initialCountdown;

    public Rigidbody player { get; private set; }
    public RaceManager raceManager { get; private set; }

    public CountdownTimerUI countdownTimerUI { get; private set; }
    public EndGamePanel endGamePanel { get; private set; }


    private State state;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Rigidbody>();
        raceManager = GameObject.Find("RaceManager").GetComponent<RaceManager>();
        countdownTimerUI = GameObject.Find("CountdownTimerUI").GetComponent<CountdownTimerUI>();
        endGamePanel = GameObject.Find("EndGamePanel").GetComponent<EndGamePanel>();
    }

    private void Start()
    {
        SetState(new InitialCountdownState(this));
    }

    public void SetState(State newState)
    {
        this.state = newState;
        StartCoroutine(this.state.Start());
    }


    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }

}
