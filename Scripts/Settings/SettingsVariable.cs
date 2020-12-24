using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{

	[ExecuteAlways]
	public abstract class SettingsVariable<T> : GameEvent where T : class
	{
		/// <summary>
		/// Value for template
		/// </summary>
		[SerializeField]
		private T _value = default;

		public T V
		{
			get { return _value; }
			private set { _value = value; }
		}

		/// <summary>
		/// Serialization and deserialization 
		/// </summary>
		[SerializeField]
		private SettingsScriptableObject _settings;

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
