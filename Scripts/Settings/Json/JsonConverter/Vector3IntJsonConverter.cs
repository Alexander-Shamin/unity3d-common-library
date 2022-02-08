using System;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Common
{
public class Vector3IntJsonConverter : JsonConverter<Vector3Int>
{
  public override Vector3Int ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType,
                                      Vector3Int existingValue, bool hasExistingValue,
                                      JsonSerializer serializer)
  {
    if (reader.TokenType == JsonToken.StartObject)
    {
      JObject item = JObject.Load(reader);
      existingValue.x = item["X"].Value<int>();
      existingValue.y = item["Y"].Value<int>();
      existingValue.z = item["Z"].Value<int>();
    }
    else
    {
      existingValue.x = reader.ReadAsInt32().Value;
      existingValue.y = reader.ReadAsInt32().Value;
      existingValue.z = reader.ReadAsInt32().Value;
    }
    return existingValue;
  }

  public override void WriteJson(JsonWriter writer, Vector3Int value, JsonSerializer serializer)
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
