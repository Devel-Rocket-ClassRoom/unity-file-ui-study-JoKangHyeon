using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

public class QuaternionConverter : JsonConverter<Quaternion>
{
    public override Quaternion ReadJson(JsonReader reader, Type objectType, Quaternion existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        Quaternion v = Quaternion.identity;

        JObject jObj = JObject.Load(reader);
        v.x = (float)jObj["X"];
        v.y = (float)jObj["Y"];
        v.z = (float)jObj["Z"];
        v.w = (float)jObj["W"];

        return v;
    }

    public override void WriteJson(JsonWriter writer, Quaternion value, JsonSerializer serializer)
    {
        JObject jObj = new JObject
        {
            ["X"] = value.x,
            ["Y"] = value.y,
            ["Z"] = value.z,
            ["W"] = value.w
        };
        
        jObj.WriteTo(writer);
    }
}


public class ColorConverter : JsonConverter<Color>
{
    public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        Color v = Color.white;

        JObject jObj = JObject.Load(reader);
        v.r = (float)jObj["R"];
        v.g = (float)jObj["G"];
        v.b = (float)jObj["B"];
        v.a = (float)jObj["A"];

        return v;
    }

    public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
    {
        JObject jObj = new JObject
        {
            ["R"] = value.r,
            ["G"] = value.g,
            ["B"] = value.b,
            ["A"] = value.a
        };

        jObj.WriteTo(writer);
    }
}