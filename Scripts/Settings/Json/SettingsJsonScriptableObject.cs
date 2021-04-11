using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Common
{
	/// <summary>
	///	Класс реализующий хранение данных в Json файле
	/// </summary>
	/// <remarks>
	/// Класс реализован в виде ScriptableObject и независим от сцены использования.
	/// Класс представляет хранение, согласно интерфейсу AbstractSettings
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
						localStorage = new JsonFileStorage(Application.streamingAssetsPath, localFilename, JsonSerializer);
					return localStorage;

				default:
				case TypeScopeSettings.Global:
					{
						if (globalStorage == null)
							globalStorage = new JsonFileStorage(Application.streamingAssetsPath, globalFilename, JsonSerializer);
						return globalStorage;
					}
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
						Formatting = Formatting.Indented
					};
					_jsonSerializer.Converters.Add(new ColorJsonConverter());
					_jsonSerializer.Converters.Add(new Matrix4x4JsonConverter());
					_jsonSerializer.Converters.Add(new Vector2JsonConverter());
					_jsonSerializer.Converters.Add(new Vector2IntJsonConverter());
					_jsonSerializer.Converters.Add(new Vector3JsonConverter());
					_jsonSerializer.Converters.Add(new Vector3IntJsonConverter());
					_jsonSerializer.Converters.Add(new StringEnumConverter());
					return _jsonSerializer;
				}
			}
		}

		public override T GetValue<T>(string name, T defaultValue = default, TypeScopeSettings scope = TypeScopeSettings.Global)
		{
			try
			{
				return (T)GetStorage(scope).Storage[name];
			}
			catch (KeyNotFoundException exception)
			{
				Debug.LogException(exception);
				SetValue(name, defaultValue, scope);
			}
			catch (InvalidCastException exception)
			{
				Debug.LogException(exception);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				return GetStorage(scope).Storage[name].ToObject(typeof(T), JsonSerializer);
			}
			return defaultValue;
		}

		public override void SetValue<T>(string name, T value, TypeScopeSettings scope = TypeScopeSettings.Global)
		{
			TryAdd(GetStorage(scope).Storage, name, value);
		}

		private void TryAdd<T>(Dictionary<string, dynamic> dict, string name, T value)
		{
			if (dict.ContainsKey(name))
				dict[name] = value;
			else
				dict.Add(name, value);
		}

		public override void Load()
		{
			GetStorage(TypeScopeSettings.Local).Deserialize();
			GetStorage(TypeScopeSettings.Global).Deserialize();
			InvokeOnSettingsChanged();
		}


		public override void Save()
		{
			GetStorage(TypeScopeSettings.Local).Serialize();
			GetStorage(TypeScopeSettings.Global).Serialize();
		}
	} // class
}
