using Catalog.API.DTO;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Kafka;

namespace Catalog.API.Services;

public class PocOrderService : IPocOrderService
{
    private readonly IKafkaProducer _kafkaProducer;
    
    public PocOrderService(IKafkaProducer kafkaProducer)
    {
        _kafkaProducer = kafkaProducer;
    }
    public async Task CreateOrder(PocOrder order)
    {
        await _kafkaProducer.ProduceAsync<PocOrder>("topic", "key", order);
    }
}