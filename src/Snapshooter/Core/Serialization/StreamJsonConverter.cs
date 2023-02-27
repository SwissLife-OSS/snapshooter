using System;
using System.IO;
using Newtonsoft.Json;

namespace Snapshooter.Core.Serialization;

public class StreamJsonConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return typeof(Stream).IsAssignableFrom(objectType);
    }

    public override object ReadJson(
        JsonReader reader,
        Type objectType,
        object existingValue,
        JsonSerializer serializer)
    {
        var bytes = serializer.Deserialize<byte[]>(reader);

        return bytes != null ?
            new MemoryStream(bytes) :
            new MemoryStream();
    }

    public override void WriteJson(
        JsonWriter writer,
        object value,
        JsonSerializer serializer)
    {
        using var stream = (Stream)value;

        byte[] bytes = new byte[stream.Length];
        stream.Read(bytes, 0, bytes.Length);

        serializer.Serialize(writer, bytes);
    }
}
