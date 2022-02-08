using System;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Common
{
public class Vector4JsonConverter : JsonConverter<Vector4>
{
  public override Vector4 ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType,
                                   Vector4 existingValue, bool hasExistingValue,
                                   JsonSerializer serializer)
  {
    if (reader.TokenType == JsonToken.StartObject)
    {
      JObject item = JObject.Load(reader);
      existingValue.x = item["X"].Value<float>();
      existingValue.y = item["Y"].Value<float>();
      existingValue.z = item["Z"].Value<float>();
      existingValue.w = item["W"].Value<float>();
    }
    else
    {
      existingValue.x = (float)reader.ReadAsDouble().Value;
      existingValue.y = (float)reader.ReadAsDouble().Value;
      existingValue.z = (float)reader.ReadAsDouble().Value;
      existingValue.w = (float)reader.ReadAsDouble().Value;
    }
    return existingValue;
  }

  public override void WriteJson(JsonWriter writer, Vector4 value, JsonSerializer serializer)
  {
    writer.WriteStartObject();
    writer.WritePropertyName("X");
    writer.WriteValue(value.x);
    writer.WritePropertyName("Y");
    writer.WriteValue(value.y);
    writer.WritePropertyName("Z");
    writer.WriteValue(value.z);
    writer.WritePropertyName("W");
    writer.WriteValue(value.w);
    writer.WriteEndObject();
  }
}
}  // namespace
