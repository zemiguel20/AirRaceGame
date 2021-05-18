using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameState;

public class GameManager : MonoBehaviour
{
    public int initialCountdown;

    public Rigidbody player { get; private set; }
    public RaceManager raceManager { get; private set; }

    private State state;


    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Rigidbody>();
        raceManager = GameObject.Find("RaceManager").GetComponent<RaceManager>();
    }

    private void Start()
    {
        SetState(new InitialCountdownState(this, initialCountdown));
    }

    public void SetState(State newState)
    {
        this.state = newState;
        StartCoroutine(this.state.Start());
    }

}
