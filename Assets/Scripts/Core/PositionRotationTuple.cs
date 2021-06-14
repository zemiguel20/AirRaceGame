using UnityEngine;

namespace AirRace.Core
{
    public class PositionRotationTuple
    {
        public Vector3 Position { get; private set; }
        public Quaternion Rotation { get; private set; }

        public PositionRotationTuple(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }
}