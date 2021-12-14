using System;
using UnityEngine;

namespace AirRace
{
    public class Goal : MonoBehaviour
    {
        public static event Action<Goal> passed;

        private void OnTriggerEnter(Collider other)
        {
            passed?.Invoke(this);
        }
    }
}
