using System;
using Confluent.Kafka;
using MessagePack;

namespace MusicCollection.Models.Models.Serialization;

public class MsgPackSerializer<T> : ISerializer<T>
{
    public byte[] Serialize(T data, SerializationContext context)
    {
        return MessagePackSerializer.Serialize(data);
    }
}
