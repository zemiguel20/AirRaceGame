using UnityEngine;

namespace AirRace.Player
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform player;
        public float offsety;
        public float offsetz;
        public float smoothTime;

        private Vector3 velocity = Vector3.zero;

        // FIXME - SmoothDamp jitter only gone in FixedUpdate?????
        void FixedUpdate()
        {
            Vector3 target = player.position + player.up * offsety + player.forward * offsetz;
            transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);
            transform.LookAt(player);
        }
    }
}