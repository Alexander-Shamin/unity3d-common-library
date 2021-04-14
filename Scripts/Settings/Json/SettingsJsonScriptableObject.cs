using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Common
{
	/// <summary>
	///	Класс реализующий хранение данных в Json файлах (default name: local_settings.json (локальное), settings.json (глобальное))
	///   что позволяет реализовать контекст переключения операций 
	/// </summary>
	/// <remarks>
	/// Класс реализован в виде ScriptableObject и независим от сцены использования.
	/// Класс представляет хранение, согласно интерфейсу AbstractSettings
	/// Класс реализует замещение глобальных настроек локальными - для этого необходимо изменить параметр
	/// 	dominationLocalSettings = на true. В этом случае при обращении к глобальному хранилищу сначала будет 
	/// производится поиск в локальном. 
	/// </remarks>
	[CreateAssetMenu(fileName = "SettingsJson", menuName = "Common/Settings Provider/SettingsJson")]
	public class SettingsJsonScriptableObject : AbstractSettings
	{
		private JsonFileStorage localStorage = null;
		private JsonFileStorage globalStorage = null;

		private JsonFileStorage GetStorage(TypeScopeSettings scope)
		{
			switch (scope)
			{
				case TypeScopeSettings.Local:
					if (localStorage == null)
						localStorage = new JsonFileStorage(Application.streamingAssetsPath, localFilename, JsonSerializer, converters);
					return localStorage;

				default:
				case TypeScopeSettings.Global:
					{
						if (globalStorage == null)
							globalStorage = new JsonFileStorage(Application.streamingAssetsPath, globalFilename, JsonSerializer, converters);
						return globalStorage;
					}
			}
		}

		[SerializeField]
		private bool? _dominationLocalStorage = null;
		private bool DominationLocalStorage
		{
			get
			{
				if (!_dominationLocalStorage.HasValue)
					_dominationLocalStorage = GetStorage(TypeScopeSettings.Local).GetValue<bool>("dominationLocalSettings", false);

				return _dominationLocalStorage.Value;
			}
		}

		[SerializeField]
		protected string globalFilename = "settings.json";

		[SerializeField]
		protected string localFilename = "local_settings.json";

		/// <summary>
		/// JsonSerializer with our converters  
		/// </summary>
		private JsonSerializer _jsonSerializer = null;
		/// <summary>
		/// JsonSerializer properties - control time creation 
		/// </summary>
		protected JsonSerializer JsonSerializer
		{
			get
			{
				if (_jsonSerializer != null)
					return _jsonSerializer;
				else
				{
					_jsonSerializer = new JsonSerializer
					{
						Formatting = Formatting.Indented,
					};
					return _jsonSerializer;
				}
			}
		}

		private JsonConverter[] converters =
		{
			new ColorJsonConverter(),
			new Matrix4x4JsonConverter(),
			new Vector2JsonConverter(),
			new Vector2IntJsonConverter(),
			new Vector3JsonConverter(),
			new Vector3IntJsonConverter(),
			new StringEnumConverter()
		};

		public override T GetValue<T>(string name, T defaultValue = default, TypeScopeSettings scope = TypeScopeSettings.Global)
		{
			try
			{
				if (DominationLocalStorage)
				{
					if (GetStorage(TypeScopeSettings.Local).ContainstKey(name))
						return GetStorage(TypeScopeSettings.Local).GetValue<T>(name, defaultValue);
					else
						return GetStorage(TypeScopeSettings.Global).GetValue<T>(name, defaultValue);
				}
				else
					return GetStorage(scope).GetValue<T>(name, defaultValue);
			}
			catch (InvalidCastException exception)
			{
				Debug.LogException(exception);
			}
			return defaultValue;
		}

		public override void SetValue<T>(string name, T value, TypeScopeSettings scope = TypeScopeSettings.Global)
		{
			GetStorage(scope).SetValue(name, value);
		}

		public override void Load()
		{
			GetStorage(TypeScopeSettings.Local).Deserialize();
			GetStorage(TypeScopeSettings.Global).Deserialize();
			_dominationLocalStorage = GetValue<bool>("dominationLocalSettings", false, scope: TypeScopeSettings.Local);
			InvokeOnSettingsChanged();
		}


		public override void Save()
		{
			SetValue<bool>("dominationLocalSettings", DominationLocalStorage, scope: TypeScopeSettings.Local);
			GetStorage(TypeScopeSettings.Local).Serialize();
			GetStorage(TypeScopeSettings.Global).Serialize();
		}
	} // class
}
