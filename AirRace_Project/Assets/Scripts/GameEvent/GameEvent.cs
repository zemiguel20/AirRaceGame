using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
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
        this.listeners.Add(listener);
    }

    public void Unsubscribe(GameEventListener listener)
    {
        this.listeners.Remove(listener);
    }
}
