using UnityEngine;

namespace AirRace.Core.SOVariables
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Variables/FloatVariable")]
    public class FloatVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [TextArea] public string description;
#endif
        public float value;
    }
}