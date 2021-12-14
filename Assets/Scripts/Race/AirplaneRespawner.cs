using System.Collections;
using UnityEngine;

namespace AirRace
{
    public class AirplaneRespawner : MonoBehaviour
    {
        private AirplanePhysics airplanePhysics;
        private Transform airplaneTransform;

        private Vector3 respawnPoint;
        private Quaternion respawnRotation;

        private void Awake()
        {
            airplanePhysics = FindObjectOfType<AirplanePhysics>();
            airplaneTransform = airplanePhysics.transform;
        }

        private void Start()
        {
            respawnPoint = airplaneTransform.position;
            respawnRotation = airplaneTransform.rotation;

            AirplaneCollision.collided += OnAirplaneCollision;
            Goal.passed += OnGoalPassed;
        }

        private void OnAirplaneCollision()
        {
            StartCoroutine(RespawnSequence());
        }

        private IEnumerator RespawnSequence()
        {
            airplanePhysics.SetEnabled(false);

            yield return new WaitForSeconds(0.3f);

            airplaneTransform.position = respawnPoint;
            airplaneTransform.rotation = respawnRotation;

            yield return new WaitForSeconds(0.3f);

            airplanePhysics.SetEnabled(true);
        }

        private void OnGoalPassed(Goal goal)
        {
            //Update the respawn transform to the passed goal
            respawnPoint = goal.transform.position;
            respawnRotation = goal.transform.rotation;
        }

        //Cleanup on object destroy
        private void OnDestroy()
        {
            //Unsub from events
            AirplaneCollision.collided -= OnAirplaneCollision;
            Goal.passed -= OnGoalPassed;
        }
    }
}
