using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{

    public abstract class SettingsVariable<T> : EventSO where T : class
    {
        /// <summary>
        /// Value for template
        /// </summary>
        [SerializeField]
        private T _value = default(T);

        public T V
        {
            get { return _value; }
            private set { _value = value; }
        }

        /// <summary>
        /// Serialization and deserialization 
        /// </summary>
        [SerializeField]
        private SettingsSO _settings;

        [SerializeField]
        private GameEvent _gameEvent;

        public GameEvent Event
        {
            get { return _gameEvent; }
            private set { _gameEvent = value; }
        }

        /// <summary>
        /// Raise a value change event 
        /// </summary>
        public override void Raise()
        {
            _gameEvent?.Raise();
        }

        /// <summary>
        /// Unity. Ctor
        /// </summary>
        private void OnEnable()
        {
            Deserialize();
        }

        /// <summary>
        /// Unity. dctor
        /// </summary>
        private void OnDisable()
        {
            Serialize();
        }

        /// <summary>
        /// Send event, when change value in Editor
        /// </summary>
        private void OnValidate()
        {
            _gameEvent?.Raise();
        }

        /// <summary>
        /// Serialize value
        /// </summary>
        public void Serialize()
        {
            _settings?.Serialize<T>(_value);
        }

        /// <summary>
        /// Deserialize value
        /// </summary>
        public void Deserialize()
        {
            _value = _settings?.Deserialize<T>() ?? _value;
        }
    }
}
