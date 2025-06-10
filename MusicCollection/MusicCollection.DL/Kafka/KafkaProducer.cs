using System;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using MusicCollection.Models.Configurations;
using MusicCollection.Models.DTO;
using MusicCollection.Models.Models.Serialization;

namespace MusicCollection.DL.Kafka.KafkaCache;

internal class KafkaProducer<TKey, TData, TConfiguration> : IKafkaProducer<TData> where TData : ICacheItem<TKey> where TKey : notnull
    where TConfiguration : CacheConfiguration
{
    private readonly ProducerConfig _config;
    private readonly IProducer<TKey, TData> _producer;
    private readonly IOptionsMonitor<TConfiguration> _kafkaConfig;
    
    public KafkaProducer(IOptionsMonitor<TConfiguration> kafkaConfig)
    {
        _config = new ProducerConfig()
        {
            BootstrapServers = "localhost:9092",
            AllowAutoCreateTopics = true,
            EnableSslCertificateVerification = false
        };

        _producer = new ProducerBuilder<TKey, TData>(_config)
            .SetValueSerializer(new MsgPackSerializer<TData>())
            .Build();
        _kafkaConfig = kafkaConfig;
    }

    public async Task Produce(TData message)
    {
        await _producer.ProduceAsync(_kafkaConfig.CurrentValue.Topic, new Message<TKey, TData>
        {
            Key = message.GetKey(),
            Value = message
        });
    }

    public async Task ProduceAll(IEnumerable<TData> messages)
    {

        await ProduceBatches(messages);
    }

    public async Task ProduceBatches(IEnumerable<TData> messages)
    {
        const int batchSize = 50;
        var batch = new List<Task>();

        foreach (var message in messages)
        {
            batch.Add(Produce(message));

            if (batch.Count == batchSize)
            {
                await Task.WhenAll(batch);
                batch.Clear();
            }
        }
        
        if (batch.Count > 0)
        {
            await Task.WhenAll(batch);
        }
    }
}
