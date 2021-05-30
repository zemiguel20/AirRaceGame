using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Variables/FloatVariable")]
public class FloatVariable : ScriptableObject
{
    [TextArea] public string description;
    public float value;
}
