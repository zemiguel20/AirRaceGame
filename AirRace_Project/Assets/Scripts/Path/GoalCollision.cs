using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GoalHitter")
            // TODO - implement callback
            Debug.LogError("Goal hit callback not implemented!");
    }
}
