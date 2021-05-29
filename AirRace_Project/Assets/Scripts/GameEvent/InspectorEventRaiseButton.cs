using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameEvent))]
public class InspectorEventRaiseButton : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameEvent myGameEvent = (GameEvent)target;

        if (GUILayout.Button("Raise()"))
        {
            myGameEvent.Raise();
        }
    }
}
