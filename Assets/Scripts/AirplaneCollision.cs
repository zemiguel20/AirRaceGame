using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AirRace
{
    public class AirplaneCollision : MonoBehaviour
    {
        public static event Action collided;

        private void OnCollisionEnter(Collision collision)
        {
            collided?.Invoke();
        }
    }
}
