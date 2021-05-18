using UnityEngine;

public class Goal : MonoBehaviour
{

    private RaceManager raceManager;

    private void Awake()
    {
        raceManager = GameObject.Find("RaceManager").GetComponent<RaceManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GoalHitter")
            raceManager?.OnGoalHit();
    }
}
