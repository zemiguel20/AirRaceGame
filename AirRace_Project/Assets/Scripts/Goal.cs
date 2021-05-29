using Assets.Scripts.GameLogger;
using UnityEngine;
using UnityEngine.Events;

public class Goal : MonoBehaviour
{
    [SerializeField] private UnityEvent GoalHit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GoalHitter"))
        {
            GameLogger.Debug(this.name + " raised GoalHit");
            GoalHit.Invoke();
        }
    }
}
