using System;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using MusicCollection.Models.Models.Serialization;

namespace MusicCollection.DL.Kafka.KafkaCache;

public class KafkaCache<TKey, TValue> : BackgroundService
{
    private readonly ConsumerConfig _config;

    public KafkaCache()
    {
        _config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = $"KafkaChat-{Guid.NewGuid}",
            AutoOffsetReset = AutoOffsetReset.Earliest,
        };
}

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Task.Run(() => ConsumeMessages(stoppingToken), stoppingToken);
        return Task.CompletedTask;
    }

    private List<ConsumeResult<TKey, TValue>> messages = new List<ConsumeResult<TKey, TValue>>();
    private void ConsumeMessages(CancellationToken stoppingToken)
    {
        using (var consumer = new ConsumerBuilder<TKey, TValue>(_config)
                   .SetValueDeserializer(new MessagePackDeserializer<TValue>())
                   .Build())
        {
            consumer.Subscribe("song_cache");

            
            while (!stoppingToken.IsCancellationRequested)
            {

                var consumeResult = consumer.Consume(stoppingToken);

                if (consumeResult != null)
                {
                    Console.WriteLine(consumeResult.Message.Key);
                    messages.Add(consumeResult);
                    continue;
                }
            }
        }
    }
}
