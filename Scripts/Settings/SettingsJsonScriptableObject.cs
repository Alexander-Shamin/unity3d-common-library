using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Common
{
	[CreateAssetMenu(fileName = "SettingsJson", menuName = "Common/Settings Provider/SettingsJson")]
	public class SettingsJsonScriptableObject : AbstractSettings
	{
		[SerializeField]
		protected IDictionary<string, dynamic> parameters = null;

		protected IDictionary<string, dynamic> Parameters
		{
			get
			{
				if (parameters == null)
					parameters = DeserializeDictionary(path2file);
				return parameters;
			}
		}

		[SerializeField]
		protected string path = Application.streamingAssetsPath;

		[SerializeField]
		protected string filename = "settings.json";

		protected string path2file { get { return path + System.IO.Path.DirectorySeparatorChar + filename; } }

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

		public override T GetValue<T>(string name, T defaultValue = default)
		{
			try
			{
				return (T)Parameters[name];
			}
			catch (KeyNotFoundException exception)
			{
				Debug.LogException(exception);
				SetValue(name, defaultValue);
			}
			catch (InvalidCastException exception)
			{
				Debug.LogException(exception);
			}
			return defaultValue;
		}

		public override void SetValue<T>(string name, T value)
		{
			if (Parameters.ContainsKey(name))
			{
				Parameters[name] = value;
			}
			else
			{
				Parameters.Add(name, value);
			}
		}

		public override void Load()
		{
			parameters = DeserializeDictionary(path2file);
			InvokeOnSettingsChanged();
		}

		/// <summary>
		/// Deserialize all data from file as dictionary object
		/// </summary>
		private IDictionary<string, object> DeserializeDictionary(string filename)
		{
			IDictionary<string, dynamic> rtrnDictionary = null;
			try
			{
				using (System.IO.StreamReader file = System.IO.File.OpenText(filename))
				{
					rtrnDictionary = JsonSerializer.Deserialize(file, typeof(IDictionary<string, dynamic>)) as IDictionary<string, dynamic>;
					file.Close();
				}
			}
			catch
			{
				rtrnDictionary = new Dictionary<string, dynamic>();
				SerializeDicitionary(rtrnDictionary, filename);
			}
			return rtrnDictionary;
		}

		public override void Save()
		{
			SerializeDicitionary(parameters, path2file);
		}

		private void SerializeDicitionary(IDictionary<string, dynamic> obj, string filename)
		{
			try
			{
				System.IO.FileInfo fileInfo = new System.IO.FileInfo(filename);
				fileInfo.Directory.CreateSubdirectory(fileInfo.Directory.FullName);
				using (System.IO.StreamWriter file = fileInfo.CreateText())
				{
					JsonSerializer.Serialize(file, obj);
					file.Close();
				}
			}
			catch (System.Exception exception)
			{
				Debug.LogException(exception);
			}
		}

	} // class
}
