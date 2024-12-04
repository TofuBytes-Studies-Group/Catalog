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
        CatalogResponse catalogResponse = new CatalogResponse(Guid.NewGuid(), Guid.NewGuid(), "username", new List<DishResponse>());
       
        _kafkaProducer.Setup(x => x.ProduceAsync<CatalogResponse>(
            It.IsAny<string>(), 
            It.IsAny<string>(), 
            It.IsAny<CatalogResponse>()))
            .Returns(Task.CompletedTask);
        // Act
        await _service.CreateOrder(catalogResponse);
        // Assert
        _kafkaProducer.Verify(p => p.ProduceAsync<CatalogResponse>("add.to.cart", "username", catalogResponse), Times.Once);
    }
}