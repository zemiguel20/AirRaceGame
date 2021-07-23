using UnityEngine;

namespace AirRace.Player
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlaneProperties")]
    public class PlanePropertiesSO : ScriptableObject
    {
        [SerializeField] [Tooltip("Acceleration in m/s^2")] [Min(0)] private float _maxAcceleration;
        [SerializeField] [Range(0, 1)] private float _throttleIncreasePerSecond;

        [SerializeField] [Tooltip("Torque multiplier")] [Range(0, 1)] private float _torqueMultiplier;

        [SerializeField] [Tooltip("Angle of attack at which lift is maximum and starts decreasing.")] private float _stallAngle;
        [SerializeField] [Tooltip("Max lift coeficcient when stall angle is reached")] private float _maxLiftCf;

        [SerializeField] [Tooltip("Lowest drag coefficient")] private float _minDragCf;
        [SerializeField] [Tooltip("Max drag coefficient")] private float _maxDragCf;

        public float MaxAcceleration { get => _maxAcceleration; }
        public float ThrottleIncreasePerSecond { get => _throttleIncreasePerSecond; }
        public float TorqueMultiplier { get => _torqueMultiplier; }
        public float StallAngle { get => _stallAngle; }
        public float MaxLiftCf { get => _maxLiftCf; }
        public float MinDragCf { get => _minDragCf; }
        public float MaxDragCf { get => _maxDragCf; }
    }
}


