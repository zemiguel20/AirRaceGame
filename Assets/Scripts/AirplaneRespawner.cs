using System.Collections;
using UnityEngine;

namespace AirRace
{
    public class AirplaneRespawner : MonoBehaviour
    {
        //[SerializeField] private AirplaneMovement airplaneMovement;
        //private Vector3 respawnPoint;
        //private Quaternion respawnRotation;

        //private void Start()
        //{
        //    respawnPoint = this.transform.position;
        //    respawnRotation = this.transform.rotation;
        //}

        //private void OnCollisionEnter(Collision other)
        //{
        //    StartCoroutine(RespawnSequence());
        //}

        //private IEnumerator RespawnSequence()
        //{
        //    airplaneMovement.SetEnabled(false);
        //    yield return new WaitForSeconds(0.3f);
        //    this.transform.position = respawnPoint;
        //    this.transform.rotation = respawnRotation;
        //    yield return new WaitForSeconds(0.3f);
        //    airplaneMovement.SetEnabled(true);
        //}

        //private void OnTriggerEnter(Collider other)
        //{
        //    GameObject goal = other.gameObject;
        //    respawnPoint = goal.transform.position;
        //    respawnRotation = goal.transform.rotation;
        //}
    }
}
