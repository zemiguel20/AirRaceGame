using System.Collections;
using UnityEngine;

namespace AirRace
{
    [System.Serializable]
    public class AirplaneProperties
    {
        [SerializeField] [Tooltip("Acceleration in m/s^2")] [Min(0)] private float _maxThrottle;

        [SerializeField] [Tooltip("Rotation speed in degrees/sec")] private float _rotationSpeed;

        [SerializeField] [Tooltip("Angle of attack at which lift is maximum and starts decreasing.")] private float _stallAngle;
        [SerializeField] [Tooltip("Max lift coeficcient when stall angle is reached")] private float _maxLiftCf;

        [SerializeField] [Tooltip("Lowest drag coefficient")] private float _minDragCf;
        [SerializeField] [Tooltip("Max drag coefficient")] private float _maxDragCf;

        public float MaxThrottle { get => _maxThrottle; }
        public float RotationSpeed { get => _rotationSpeed; }
        public float StallAngle { get => _stallAngle; }
        public float MaxLiftCf { get => _maxLiftCf; }
        public float MinDragCf { get => _minDragCf; }
        public float MaxDragCf { get => _maxDragCf; }
    }
}