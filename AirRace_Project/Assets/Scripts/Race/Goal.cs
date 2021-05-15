using UnityEngine;

public class Goal : MonoBehaviour
{

    public RaceManager raceManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GoalHitter")
            raceManager?.OnGoalHit();
    }
}
