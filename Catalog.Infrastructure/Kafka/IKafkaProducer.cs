using Catalog.Domain.Entities;

namespace Catalog.Infrastructure.Kafka;

public interface IKafkaProducer
{
    public Task ProduceAsync<T>(string topic, string key, PocOrder value);
}