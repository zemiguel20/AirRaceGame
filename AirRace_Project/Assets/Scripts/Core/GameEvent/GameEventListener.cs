using UnityEngine;
using UnityEngine.Events;

namespace AirRace.Core.GameEvent
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] private GameEvent Event;
        [SerializeField] private UnityEvent Response;

        private void OnEnable()
        {
            Event.Subscribe(this);
        }

        private void OnDisable()
        {
            Event.Unsubscribe(this);
        }

        public void OnEventRaised()
        {
            Response.Invoke();
        }
    }
}