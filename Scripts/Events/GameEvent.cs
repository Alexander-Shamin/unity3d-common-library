using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    [CreateAssetMenu(fileName = "Game Event",
        menuName = "Common/Event/GameEvent")]
    public class GameEvent : EventScriptableObject
    {
        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        [SerializeField]
        private readonly List<GameEventListener> eventListeners =
            new List<GameEventListener>();

        /// <summary>
        /// Для подписок через код
        /// </summary>
        public event System.Action OnEventRaised;

        public override void Raise()
        {
            OnEventRaised?.Invoke();
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
}
