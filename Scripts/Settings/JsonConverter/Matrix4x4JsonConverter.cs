using System;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Common
{
	public class Matrix4x4JsonConverter : JsonConverter<Matrix4x4>
	{
		public override Matrix4x4 ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, Matrix4x4 existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.StartObject)
			{
				JObject item = JObject.Load(reader);
				StringBuilder str = new StringBuilder(5); 
				for (int iy = 0; iy < 4; ++iy)
				{
					for (int ix = 0; ix < 4; ++ix)
					{
						str.Append($"m{iy}{ix}");
						JToken value;
						if(item.TryGetValue(str.ToString(), out value))
						{
							existingValue[iy, ix] = value.Value<float>();
						}
						str.Clear();
					}
				}
			}
			else
			{
				for (int iy = 0; iy < 4; ++iy)
				{
					for (int ix = 0; ix < 4; ++ix)
					{
						existingValue[iy, ix] = (float)reader.ReadAsDouble();
					}
				}
			}

			return existingValue;
		}

		public override void WriteJson(JsonWriter writer, Matrix4x4 value, JsonSerializer serializer)
		{
			writer.WriteStartObject();
			for (int iy = 0; iy < 4; ++iy)
			{
				for (int ix = 0; ix < 4; ++ix)
				{
					writer.WritePropertyName($"m{iy}{ix}");
					writer.WriteValue(value[iy, ix]);
				}
			}
			writer.WriteEndObject();
		}
	}
}
