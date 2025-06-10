using System;
using Confluent.Kafka;
using MessagePack;

namespace MusicCollection.Models.Models.Serialization;

public class MessagePackDeserializer<T> : IDeserializer<T>
{
    public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        return MessagePackSerializer.Deserialize<T>(data.ToArray());
    }
}
