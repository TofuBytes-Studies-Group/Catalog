

using Catalog.API.DTO;

namespace Catalog.Infrastructure.Kafka;

public interface IKafkaProducer
{
    public Task ProduceAsync<T>(string topic, string key, CatalogResponse value);
}