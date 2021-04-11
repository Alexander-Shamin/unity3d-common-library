using System;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Common
{

	public class ColorJsonConverter : JsonConverter<Color>
	{
		public override Color ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.StartObject)
			{
				JObject item = JObject.Load(reader);
				existingValue.r = item["R"].Value<float>();
				existingValue.g = item["G"].Value<float>();
				existingValue.b = item["B"].Value<float>();
				existingValue.a = item["A"].Value<float>();
			}
			else
			{
				existingValue.r = (float)reader.ReadAsDouble().Value;
				existingValue.g = (float)reader.ReadAsDouble().Value;
				existingValue.b = (float)reader.ReadAsDouble().Value;
				existingValue.a = (float)reader.ReadAsDouble().Value;
			}
			return existingValue;
		}

		public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("R");
			writer.WriteValue(value.r);
			writer.WritePropertyName("G");
			writer.WriteValue(value.g);
			writer.WritePropertyName("B");
			writer.WriteValue(value.b);
			writer.WritePropertyName("A");
			writer.WriteValue(value.a);
			writer.WriteEndObject();
		}
	}
}
