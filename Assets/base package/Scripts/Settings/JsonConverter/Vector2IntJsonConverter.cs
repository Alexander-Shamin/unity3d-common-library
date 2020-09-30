using System;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Common
{
	public class Vector2IntJsonConverter : JsonConverter<Vector2Int>
	{
		public override Vector2Int ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, Vector2Int existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.StartObject)
			{
				JObject item = JObject.Load(reader);
				existingValue.x = item["X"].Value<int>();
				existingValue.y = item["Y"].Value<int>();
			}
			else
			{
				existingValue.x = reader.ReadAsInt32().Value;
				existingValue.y = reader.ReadAsInt32().Value;
			}
			return existingValue;
		}

		public override void WriteJson(JsonWriter writer, Vector2Int value, JsonSerializer serializer)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("X");
			writer.WriteValue(value.x);
			writer.WritePropertyName("Y");
			writer.WriteValue(value.y);
			writer.WriteEndObject();
		}
	}
}
