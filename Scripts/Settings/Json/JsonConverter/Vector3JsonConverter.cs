using System;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Common
{
public class Vector3JsonConverter : JsonConverter<Vector3>
{
  public override Vector3 ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType,
                                   Vector3 existingValue, bool hasExistingValue,
                                   JsonSerializer serializer)
  {
    if (reader.TokenType == JsonToken.StartObject)
    {
      JObject item = JObject.Load(reader);
      existingValue.x = item["X"].Value<float>();
      existingValue.y = item["Y"].Value<float>();
      existingValue.z = item["Z"].Value<float>();
    }
    else
    {
      existingValue.x = (float)reader.ReadAsDouble().Value;
      existingValue.y = (float)reader.ReadAsDouble().Value;
      existingValue.z = (float)reader.ReadAsDouble().Value;
    }
    return existingValue;
  }

  public override void WriteJson(JsonWriter writer, Vector3 value, JsonSerializer serializer)
  {
    writer.WriteStartObject();
    writer.WritePropertyName("X");
    writer.WriteValue(value.x);
    writer.WritePropertyName("Y");
    writer.WriteValue(value.y);
    writer.WritePropertyName("Z");
    writer.WriteValue(value.z);
    writer.WriteEndObject();
  }
}
}
