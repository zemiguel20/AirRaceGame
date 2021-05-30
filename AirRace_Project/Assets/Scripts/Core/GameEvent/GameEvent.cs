using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/GameEvent")]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();

    public void Raise()
    {
        foreach (GameEventListener listener in listeners)
        {
            listener.OnEventRaised();
        }
    }

    public void Subscribe(GameEventListener listener)
    {
        listeners.Add(listener);
    }

    public void Unsubscribe(GameEventListener listener)
    {
        listeners.Remove(listener);
    }
}
