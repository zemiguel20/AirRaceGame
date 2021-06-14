using AirRace.Core;
using AirRace.Player;
using System.Collections;
using UnityEngine;

namespace AirRace.GameState
{
    public class PlayerRespawner : MonoBehaviour
    {
        [SerializeField] private AirplaneController _airplaneController;

        private Vector3 respawnPoint;
        private Quaternion respawnRotation;

        void Start()
        {
            PositionRotationTuple tuple = _airplaneController.PlanePositionAndRotation();
            respawnPoint = tuple.Position;
            respawnRotation = tuple.Rotation;

            _airplaneController.TerrainHit += OnTerrainHit;
        }

        private void OnDisable()
        {
            _airplaneController.TerrainHit -= OnTerrainHit;
        }

        public void UpdateRespawn(GameObject goal)
        {
            respawnPoint = goal.transform.position;
            respawnRotation = goal.transform.rotation;

            GameLogger.Debug("Respawn updated.");
        }

        private void OnTerrainHit()
        {
            StartCoroutine(Respawn());
        }

        private IEnumerator Respawn()
        {
            _airplaneController.EnablePhysics(false);

            yield return new WaitForSeconds(0.3f);

            _airplaneController.SetPlanePositionAndRotation(new PositionRotationTuple(respawnPoint, respawnRotation));

            yield return new WaitForSeconds(0.3f);

            _airplaneController.EnablePhysics(true);

        }
    }
}