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

    public UI UI { get; private set; }


    private State state;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        raceManager = GetComponent<RaceManager>();
        UI = GameObject.Find("UI").GetComponent<UI>();
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
