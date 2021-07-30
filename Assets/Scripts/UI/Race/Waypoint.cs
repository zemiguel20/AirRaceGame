using AirRace.Race;
using System.Collections;
using UnityEngine;

namespace AirRace.UI.Race.HUD
{
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] private Camera _playerCamera;
        [SerializeField] private Path _pathManager;

        public int offsetY;

        // Update is called once per frame
        void Update()
        {
            Vector3 goalPositionWorld = _pathManager.GetCurrentGoal().transform.position;

            /*
             * Target relative position to the Camera transform.
             * Waypoint "mirrors" transform of Camera, so Target is also relative to Waypoint.
             * Offset because waypoint is not on the center
             */
            Vector3 goalPositionLocalPlayerCamera = _playerCamera.transform.InverseTransformPoint(goalPositionWorld) + (_playerCamera.transform.up * offsetY);

            /*
             * LookAt looks at point in World coords.
             * So we had the Waypoint world position to the relative vector.
             * 
             */
            Vector3 target = goalPositionLocalPlayerCamera + this.transform.position ;
            this.transform.LookAt(target);
        }
    }
}