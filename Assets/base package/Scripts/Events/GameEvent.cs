using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    [CreateAssetMenu(menuName = "Common/Event/GameEvent")]
    public class GameEvent : EventSO
    {
        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        [SerializeField]
        private readonly List<GameEventListener> eventListeners =
            new List<GameEventListener>();

        public override void Raise()
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised();
        }

        public void RegisterListener(GameEventListener listener)
        {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener listener)
        {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    } // GameEvent

    /// <summary>
    /// Generic version
    /// </summary>
    public class GameEvent<T> : EventSO<T>
    {
        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        [SerializeField]
        private readonly List<GameEventListener<T>> eventListeners =
            new List<GameEventListener<T>>();

        public override void Raise(T value)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised(value);
        }

        public void RegisterListener(GameEventListener<T> listener)
        {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener<T> listener)
        {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    } // GameEvent
    
    /// <summary>
    /// Generic version
    /// </summary>
    public class GameEvent<T,V> : EventSO<T,V>
    {
        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        [SerializeField]
        private readonly List<GameEventListener<T,V>> eventListeners =
            new List<GameEventListener<T,V>>();

        public override void Raise(T value, V value2)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised(value, value2);
        }

        public void RegisterListener(GameEventListener<T,V> listener)
        {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener<T,V> listener)
        {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    } // GameEvent
}
