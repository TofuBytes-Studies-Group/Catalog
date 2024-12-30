using Catalog.DTO.DTO;
using Catalog.Infrastructure.Kafka;

namespace Catalog.API.Services;

public class PocOrderService(IKafkaProducer kafkaProducer) : IPocOrderService
{
    public async Task CreateOrder(CatalogResponse order)
    {
        await kafkaProducer.ProduceAsync<CatalogResponse>("add.to.cart", order.CustomerUsername, order);
    }
}