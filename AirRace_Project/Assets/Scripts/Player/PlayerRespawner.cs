using AirRace.Core;
using AirRace.Core.Events;
using System.Collections;
using UnityEngine;

namespace Player.Assets.Scripts.Player
{
    public class PlayerRespawner : MonoBehaviour
    {
        [SerializeField] private EventManager _eventManager;
        [SerializeField] private Rigidbody _player;

        private Vector3 respawnPoint;
        private Quaternion respawnRotation;

        void Start()
        {
            respawnPoint = _player.position;
            respawnRotation = _player.rotation;

            _eventManager.GoalPassed += UpdateRespawn;
        }

        private void OnDisable()
        {
            _eventManager.GoalPassed -= UpdateRespawn;
        }

        public void UpdateRespawn(GameObject goal)
        {
            respawnPoint = goal.transform.position;
            respawnRotation = goal.transform.rotation;

            GameLogger.Debug("Respawn updated.");
        }

        private void OnCollisionEnter(Collision collision)
        {
            StartCoroutine(Respawn());
        }

        private IEnumerator Respawn()
        {
            _player.isKinematic = true;

            yield return new WaitForSeconds(0.3f);

            _player.position = respawnPoint;
            _player.rotation = respawnRotation;

            yield return new WaitForSeconds(0.3f);

            _player.isKinematic = false;

        }
    }
}