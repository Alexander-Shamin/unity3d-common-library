using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    /// <summary>
    /// Abstract class event with ScriptableObject
    /// </summary>
    public abstract class EventSO : ScriptableObject
    {
        public abstract void Raise();
    }
    
    public abstract class EventSO<T> : ScriptableObject
    {
        public abstract void Raise(T value);
    }

    public abstract class EventSO<T,V> : ScriptableObject
    {
        public abstract void Raise(T value, V value2);
    }
}
