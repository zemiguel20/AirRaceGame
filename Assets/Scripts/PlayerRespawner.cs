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
            respawnPoint = _player.Position;
            respawnRotation = _player.Rotation;
        }

        public void UpdateRespawn(GameObject goal)
        {
            respawnPoint = goal.transform.position;
            respawnRotation = goal.transform.rotation;
        }

        public void Respawn()
        {
            _player.Position = respawnPoint;
            _player.Rotation = respawnRotation;
        }
    }
}