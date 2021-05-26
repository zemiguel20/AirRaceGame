using System.Collections;
using UnityEngine;

public class PlaneRespawner : MonoBehaviour
{

    private Vector3 respawnPosition;
    private Quaternion respawnRotation;

    private Rigidbody plane;

    private void Awake()
    {
        plane = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        respawnPosition = plane.transform.position;
        respawnRotation = plane.transform.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            StartCoroutine(Respawn());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            respawnPosition = other.transform.position;
            respawnRotation = other.transform.rotation;
        }
    }


    private IEnumerator Respawn()
    {
        plane.isKinematic = true;

        yield return new WaitForSeconds(0.3f);

        plane.transform.position = respawnPosition;
        plane.transform.rotation = respawnRotation;

        yield return new WaitForSeconds(0.3f);

        plane.isKinematic = false;
    }

}
