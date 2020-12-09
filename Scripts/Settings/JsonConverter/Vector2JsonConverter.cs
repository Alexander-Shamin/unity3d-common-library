using System;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Common
{
	public class Vector2JsonConverter : JsonConverter<Vector2>
	{
		public override Vector2 ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, Vector2 existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.StartObject)
			{
				JObject item = JObject.Load(reader);
				existingValue.x = item["X"].Value<float>();
				existingValue.y = item["Y"].Value<float>();
			}
			else
			{
				existingValue.x = (float)reader.ReadAsDouble().Value;
				existingValue.y = (float)reader.ReadAsDouble().Value;
			}
			return existingValue;
		}

		public override void WriteJson(JsonWriter writer, Vector2 value, JsonSerializer serializer)
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
