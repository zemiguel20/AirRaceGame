using UnityEngine;

namespace AirRace.Race
{
    public class PlayerRespawner
    {
        private Airplane _player;

        private Vector3 respawnPoint;
        private Quaternion respawnRotation;


        public PlayerRespawner(Airplane player)
        {
            _player = player;
            respawnPoint = _player.transform.position;
            respawnRotation = _player.transform.rotation;
        }

        public void UpdateRespawn(GameObject goal)
        {
            respawnPoint = goal.transform.position;
            respawnRotation = goal.transform.rotation;
        }

        public void Respawn()
        {
            _player.Rigidbody.position = respawnPoint;
            _player.Rigidbody.rotation = respawnRotation;
        }
    }
}