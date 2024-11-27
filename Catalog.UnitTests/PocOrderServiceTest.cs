using Catalog.API.DTO;
using Catalog.API.Services;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Kafka;
using Moq;

namespace Catalog.UnitTests;

public class PocOrderServiceTest
{
    private readonly Mock<IKafkaProducer> _kafkaProducer;
    private readonly PocOrderService _service;
    
    public PocOrderServiceTest()
    {
        _kafkaProducer = new Mock<IKafkaProducer>();
        _service = new PocOrderService(_kafkaProducer.Object);
    }
    
    [Fact]
    public async void CreateOrderShouldProduceOnKafka()
    {
        // Arrange
        PocOrder pocOrder = new PocOrder(Guid.NewGuid(), new List<Guid> { Guid.NewGuid() });
       
        _kafkaProducer.Setup(x => x.ProduceAsync<PocOrder>(
            It.IsAny<string>(), 
            It.IsAny<string>(), 
            It.IsAny<PocOrder>()))
            .Returns(Task.CompletedTask);
        // Act
        await _service.CreateOrder(pocOrder);
        // Assert
        _kafkaProducer.Verify(p => p.ProduceAsync<PocOrder>("topic", "key", pocOrder), Times.Once);
    }
}