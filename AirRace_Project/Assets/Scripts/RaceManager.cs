using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Assets.Scripts.GameState;

public class RaceManager : MonoBehaviour
{
    public List<Goal> goals;
    public float TIME_LIMIT;

    public float timeCounter { get; private set; }
    public float score { get; private set; }

    private bool raceStarted;
    private int goalsPassed;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        raceStarted = false;
    }

    private void Update()
    {
        if (raceStarted)
        {
            timeCounter += Time.deltaTime;
        }
    }

    public void OnGoalHit()
    {
        // Will be replaced with GUI
        Debug.Log("Time: " + timeCounter);

        score += CalculatePoints(timeCounter);
        timeCounter = 0;
        goalsPassed++;
        Goal passedGoal = goals[goalsPassed - 1];
        passedGoal.gameObject.SetActive(false);

        if (goalsPassed >= goals.Count)
        {
            raceStarted = false;
            gameManager.SetState(new EndGameState(gameManager));
        }
        else
        {
            Goal nextGoal = goals[goalsPassed];
            nextGoal.gameObject.SetActive(true);
        }
    }

    private float CalculatePoints(float playerTime)
    {
        float points = 0;

        if (playerTime < TIME_LIMIT)
            points = TIME_LIMIT - playerTime;


        points = (float)Math.Round(points, 1) * 10;

        return points;
    }

    public void StartRace()
    {
        if (raceStarted)
        {
            Debug.Log("Race already started");
            return;
        }

        if (goals.Count <= 0)
        {
            Debug.LogWarning("Cant start a race with no goals!");
            return;
        }

        raceStarted = true;
        goalsPassed = 0;
        timeCounter = 0;
        score = 0;

        //Turns off goals except the first one
        for (int i = 1; i < goals.Count; i++)
        {
            goals[i].gameObject.SetActive(false);
        }
    }
}
