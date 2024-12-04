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
    private readonly Mock<IDishService> _dishServiceMock;
    
    public PocOrderControllerTest()
    {
        _orderServiceMock = new Mock<IPocOrderService>();
        _dishServiceMock = new Mock<IDishService>();
        _orderController = new PocOrderController(_orderServiceMock.Object, _dishServiceMock.Object);
    }
    
    [Fact]
    public async void CreateOrderAsyncShouldReturnCreatedResult()
    {
        // Arrange
        
        var orderRequest = new CatalogRequest(new Guid(), new Guid(), "username", new List<Guid> {Guid.NewGuid()});
        _dishServiceMock.Setup(s => s.GetDish(It.IsAny<Guid>(),It.IsAny<Guid>())).ReturnsAsync(new DishResponse(new Guid(), "name", 10));
        _orderServiceMock.Setup(s => s.CreateOrder(new CatalogResponse())).Returns(Task.CompletedTask);
        // Act
        var result = await _orderController.CreateOrderAsync(orderRequest);
        // Assert
        Assert.IsType<CreatedResult>(result);
    }
    
    [Fact]
    public async void CreateOrderAsyncShouldReturnNotFoundResult()
    {
        // Arrange
        var orderRequest = new CatalogRequest(new Guid(), new Guid(), "username", new List<Guid> {Guid.NewGuid()});
        _dishServiceMock.Setup(s => s.GetDish(It.IsAny<Guid>(),It.IsAny<Guid>())).ThrowsAsync(new KeyNotFoundException());
        // Act
        var result = await _orderController.CreateOrderAsync(orderRequest);
        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
}