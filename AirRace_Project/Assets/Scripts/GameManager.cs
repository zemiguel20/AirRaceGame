using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameState;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public int initialCountdown;

    public Rigidbody player { get; private set; }
    public RaceManager raceManager { get; private set; }

    public UI UI { get; private set; }


    private State state;

    public static bool isPaused = false;

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

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseGame()
    {
        StartCoroutine(state.Pause());
    }

    public void ResumeGame()
    {
        StartCoroutine(state.Resume());
    }

    public void OnPauseGame(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }

    }
}
