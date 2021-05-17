using UnityEngine;

public class Goal : MonoBehaviour
{

    private RaceManager raceManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GoalHitter")
        {
            raceManager?.OnGoalHit();

            if (raceManager == null)
                Debug.LogWarning("No RaceManager attached.");
        }

    }

    public void SetRaceManager(RaceManager raceManager)
    {
        this.raceManager = raceManager;
    }
}
