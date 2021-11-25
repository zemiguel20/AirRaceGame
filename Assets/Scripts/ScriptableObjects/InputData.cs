using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AirRace
{
    //Player input system writes to an InputData object, and that object is used by other objects such as the airplane

    [CreateAssetMenu(menuName = "AirRace/InputData")]
    public class InputData : ScriptableObject
    {
        //Input for the rotation on each axis of the airplane
        public Vector3 RotateValues;

        //input value to increase or decrease the airplane throttle
        public float ThrottleValue;
        public event Action PauseInputTriggered;

        public void InvokePauseTriggeredEvent()
        {
            PauseInputTriggered?.Invoke();
        }
    }
}
