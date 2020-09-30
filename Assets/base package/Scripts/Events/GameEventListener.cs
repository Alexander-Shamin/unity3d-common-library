using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{

    /// <summary>
    /// Listener to Game Event
    /// 
    /// Данный класс используется, как добавочный скрипт к объекту.
    /// В нем можно указать объект события и действия, которые с ним связаны.
    /// </summary>
    public class GameEventListener : MonoBehaviour
    {
        [Tooltip("Event to register with.")]
        public GameEvent Event;

        [Tooltip("Response to invoke when Event is raised.")]
        public UnityEngine.Events.UnityEvent Response;

        private void OnEnable()
        {
            Event?.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event?.UnregisterListener(this);
        }

        public void OnEventRaised()
        {
            Response.Invoke();
        }
    }

    /// <summary>
    /// Generic version
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GameEventListener<T> : MonoBehaviour
    {
        [Tooltip("Event to register with.")]
        public GameEvent<T> Event;

        [Tooltip("Response to invoke when Event is raised.")]
        public UnityEngine.Events.UnityEvent<T> Response;

        private void OnEnable()
        {
            Event?.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event?.UnregisterListener(this);
        }

        public void OnEventRaised(T value)
        {
            Response.Invoke(value);
        }
    }

    /// <summary>
    /// Generic version
    /// </summary>
    public class GameEventListener<T,V> : MonoBehaviour
    {
        [Tooltip("Event to register with.")]
        public GameEvent<T,V> Event;

        [Tooltip("Response to invoke when Event is raised.")]
        public UnityEngine.Events.UnityEvent<T,V> Response;

        private void OnEnable()
        {
            Event?.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event?.UnregisterListener(this);
        }

        public void OnEventRaised(T value, V value2)
        {
            Response.Invoke(value, value2);
        }
    }
}
