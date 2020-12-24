using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Common
{
	using DictionaryObject =
			System.Collections.Generic.Dictionary<System.Type, dynamic>;

	/// <summary>
	/// Class for keep settings in json file
	/// </summary>
	[CreateAssetMenu(menuName = "Common/Settings/SettingsJson")]
	public class SettingsJson : SettingsScriptableObject
	{
		/// <summary>
		/// Filename for writting
		/// </summary>
		[SerializeField]
		private string _filename = "settings.json";


		/// <summary>
		/// JsonSerializer with our converters
		/// </summary>
		private JsonSerializer _jsonSerializer = null;
		/// <summary>
		/// JsonSerializer properties - control time creation 
		/// </summary>
		JsonSerializer JsonSerializer
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

		/// <summary>
		/// Write data to Json file
		/// </summary>
		public override void Serialize<T>(T data)
		{
			var dict = DeserializeDictionary(_filename);

			if (dict.ContainsKey(typeof(T)))
			{
				dict[typeof(T)] = data;
			}
			else
			{
				dict.Add(typeof(T), data);
			}
			SerializeDicitionary(dict, _filename);
		}

		/// <summary>
		/// Read data from json file
		/// </summary>
		public override T Deserialize<T>()
		{
			var dict = DeserializeDictionary(_filename);
			if (dict.ContainsKey(typeof(T)))
			{
				T data = dict[typeof(T)].ToObject<T>();
				return data;
			}
			else
			{
				dict.Add(typeof(T), default(T));
				SerializeDicitionary(dict, _filename);
				return default(T);
			}
		}

		/// <summary>
		/// Deserialize all data from file as dictionary object
		/// </summary>
		private DictionaryObject DeserializeDictionary(string filename)
		{
			DictionaryObject rtrnDictionary = null;
			try
			{
				using (System.IO.StreamReader file =
						System.IO.File.OpenText(filename))
				{
					rtrnDictionary = JsonSerializer.Deserialize(
							file, typeof(DictionaryObject)) as DictionaryObject;
					file.Close();
				}
			}
			catch (System.IO.FileNotFoundException exception)
			{
				Debug.LogException(exception);
				rtrnDictionary = new DictionaryObject();
				SerializeDicitionary(rtrnDictionary, filename);
			}
			return rtrnDictionary;
		}

		/// <summary>
		/// Serialize dictionary object in file
		/// </summary>
		private void SerializeDicitionary(DictionaryObject obj, string filename)
		{
			try
			{
				using (System.IO.StreamWriter file =
						System.IO.File.CreateText(filename))
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
