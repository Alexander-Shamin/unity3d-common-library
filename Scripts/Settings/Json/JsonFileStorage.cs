using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Common
{
	using BaseDictionary = Dictionary<string, string>;

	public class JsonFileStorage
	{

		public JsonFileStorage(string path, string filename, JsonSerializer serializer, JsonConverter[] converters)
		{
			this.path = path;
			this.filename = filename;
			this.serializer = serializer;
			this.converters = converters;
		}

		public void Deserialize()
		{
			storage = DeserializeDictionary(path2file);
		}

		public void Serialize()
		{
			SerializeDicitionary(Storage, path2file);
		}

		private readonly string path;
		private readonly string filename;
		private string path2file { get { return path + System.IO.Path.DirectorySeparatorChar + filename; } }
		private readonly JsonSerializer serializer;

		private readonly JsonConverter[] converters = null;


		private BaseDictionary storage = null;

		public BaseDictionary Storage
		{
			get
			{
				if (storage == null)
					storage = DeserializeDictionary(path2file);
				return storage;
			}
		}

		/// <summary>
		/// Deserialize all data from file as dictionary object
		/// </summary>
		private BaseDictionary DeserializeDictionary(string filename)
		{
			BaseDictionary rtrnDictionary = null;
			try
			{
				using (System.IO.StreamReader file = System.IO.File.OpenText(filename))
				{
					rtrnDictionary = serializer.Deserialize(file, typeof(BaseDictionary)) as BaseDictionary;
					file.Close();
				}
			}
			catch
			{
				rtrnDictionary = new BaseDictionary();
				SerializeDicitionary(rtrnDictionary, filename);
			}
			return rtrnDictionary;
		}

		private void SerializeDicitionary(BaseDictionary obj, string filename)
		{
			try
			{
				System.IO.FileInfo fileInfo = new System.IO.FileInfo(filename);
				fileInfo.Directory.CreateSubdirectory(fileInfo.Directory.FullName);
				using (System.IO.StreamWriter file = fileInfo.CreateText())
				{
					serializer.Serialize(file, obj);
					file.Close();
				}
			}
			catch (System.Exception exception)
			{
				Debug.LogException(exception);
			}
		}

		public T GetValue<T>(string name, T defaultValue = default)
		{
			try
			{
				if (Storage.ContainsKey(name))
					return JsonConvert.DeserializeObject<T>(Storage[name], converters);
				else
				{
					SetValue<T>(name, defaultValue);
					return defaultValue;
				}
			}
			catch (InvalidCastException exception)
			{
				Debug.LogException(exception);
			}
			return defaultValue;
		}

		public void SetValue<T>(string name, T value)
		{
			if (Storage.ContainsKey(name))
				Storage[name] = JsonConvert.SerializeObject(value, converters);
			else
				Storage.Add(name, JsonConvert.SerializeObject(value, converters));
		}

		public bool ContainstKey(string name)
		{
			return Storage.ContainsKey(name);
		}

	} // class 
}
