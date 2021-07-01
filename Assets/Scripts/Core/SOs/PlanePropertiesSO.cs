using UnityEngine;

namespace AirRace.Core.SOs
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlaneProperties")]
    public class PlanePropertiesSO : ScriptableObject
    {
        [SerializeField] [Tooltip("Acceleration in m/s^2")] [Min(0)] private float _maxAcceleration;

        [SerializeField] [Tooltip("Force multiplier on Roll axis")] [Min(0)] private float _rollForceMultiplier;
        [SerializeField] [Tooltip("Force multiplier on Pitch axis")] [Min(0)] private float _pitchForceMultiplier;
        [SerializeField] [Tooltip("Force multiplier on Yaw axis")] [Min(0)] private float _yawForceMultiplier;

        [SerializeField] [Tooltip("Angle of attack at which lift is maximum and starts decreasing.")] private float _stallAngle;
        [SerializeField] [Tooltip("Max lift coeficcient when stall angle is reached")] private float _maxLiftCf;

        [SerializeField] [Tooltip("Lowest drag coefficient")] private float _minDragCf;
        [SerializeField] [Tooltip("Max drag coefficient")] private float _maxDragCf;

        [SerializeField] [Tooltip("Total wing area in m^2")] private float _wingArea;

        public float MaxAcceleration { get => _maxAcceleration; }
        public float RollForceMultiplier { get => _rollForceMultiplier; }
        public float PitchForceMultiplier { get => _pitchForceMultiplier; }
        public float YawForceMultiplier { get => _yawForceMultiplier; }
        public float StallAngle { get => _stallAngle; }
        public float MaxLiftCf { get => _maxLiftCf; }
        public float MinDragCf { get => _minDragCf; }
        public float MaxDragCf { get => _maxDragCf; }
        public float WingArea { get => _wingArea; }
    }
}


