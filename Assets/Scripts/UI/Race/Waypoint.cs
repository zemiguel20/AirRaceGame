using AirRace.Race;
using System.Collections;
using UnityEngine;

namespace AirRace.UI
{
    public class Waypoint : MonoBehaviour
    {
        private Transform _player;
        private Camera _playerCamera;
        private Path _path;

        public void Initialize(Transform player, Camera playerCamera, Path path)
        {
            _player = player;
            _path = path;
            _playerCamera = playerCamera;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 goalWorldPosition = _path.GetCurrentGoal().transform.position;
            Vector3 playerWorldPosition = _player.position;

            Vector3 goalLocalPositionRelativeToCamera = _playerCamera.transform.InverseTransformPoint(goalWorldPosition);
            Vector3 playerLocalPositionRelativeToCamera = _playerCamera.transform.InverseTransformPoint(playerWorldPosition);

            /*
             * Waypoint "mirrors" Camera position.
             * Target position relative to Waypoint.
             */
            Vector3 target = goalLocalPositionRelativeToCamera - playerLocalPositionRelativeToCamera + this.transform.position;
            transform.LookAt(target);
        }
    }
}