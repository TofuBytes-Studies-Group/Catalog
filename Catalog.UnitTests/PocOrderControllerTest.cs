using Catalog.API.Controllers;
using Catalog.API.DTO;
using Catalog.API.Services;
using Catalog.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Catalog.UnitTests;

public class PocOrderControllerTest
{
    private readonly Mock<IPocOrderService> _orderServiceMock;
    private readonly PocOrderController _orderController;
    
    public PocOrderControllerTest()
    {
        _orderServiceMock = new Mock<IPocOrderService>();
        _orderController = new PocOrderController(_orderServiceMock.Object);
    }
    
    [Fact]
    public async void CreateOrderAsyncShouldReturnCreatedResult()
    {
        // Arrange
        var orderRequest = new PocOrderDTO(new Guid(), new List<Guid>());
        _orderServiceMock.Setup(s => s.CreateOrder(new PocOrder())).Returns(Task.CompletedTask);
        // Act
        var result = await _orderController.CreateOrderAsync(orderRequest);
        // Assert
        Assert.IsType<CreatedResult>(result);
    }
}