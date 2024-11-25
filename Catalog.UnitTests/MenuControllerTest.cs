using Catalog.API.Controllers;
using Catalog.API.DTO;
using Catalog.API.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Catalog.UnitTests;

public class MenuControllerTest
{
    private readonly Mock<IMenuService> _mockService;
    private readonly MenuController _controller;
    
    public MenuControllerTest()
    {
        _mockService = new Mock<IMenuService>();
        _controller = new MenuController(_mockService.Object);
    }
    
    [Fact]
    public async void GetMenuShouldReturnMenuResponse()
    {
        // Arrange
        var restaurantId = Guid.NewGuid();
        var menu = new MenuResponse(new Guid(), restaurantId, new List<DishResponse>());
        _mockService.Setup(s => s.GetMenu(restaurantId)).ReturnsAsync(menu);
        // Act
        var result = await _controller.GetMenuAsync(restaurantId);
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsType<MenuResponse>(okResult.Value);
        Assert.Equal(menu.RestaurantId, model.RestaurantId);
        
    }
    
    [Fact]
    public async void GetMenuShouldReturnNotFoundWhenMenuIsNotFound()
    {
        // Arrange
        var restaurantId = Guid.NewGuid();
        _mockService.Setup(s => s.GetMenu(restaurantId)).Throws(new KeyNotFoundException());
        // Act
        var result = await _controller.GetMenuAsync(restaurantId);
        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("The given restaurant might not exist or it does not have a menu", notFoundResult.Value);
    }
    
}