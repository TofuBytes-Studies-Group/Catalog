using Catalog.API.Controllers;
using Catalog.API.DTO;
using Catalog.API.Services;
using Catalog.API.Validators;
using Catalog.Domain.Entities;
using Catalog.Infrastructure;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.IntegrationTests;

public class MenuIntegrationTest : IDisposable
{
    private readonly CatalogContext _dbContext;
    private readonly MenuService _service;
    private readonly MenuController _controller;

    public MenuIntegrationTest()
    {
        var options = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: "RestaurantDbTest")
            .Options;
        _dbContext = new CatalogContext(options);
        _service = new MenuService(_dbContext);
        _controller = new MenuController(_service);
    }
    
    public void Dispose()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }

    [Fact]
    public async void GetMenuShouldReturnOkResultWithMenuWithListOfDishes()
    {
        // Arrange
        var address = new Address("TestStreet", "TestCity", 100);
        var restaurant = new Restaurant("TestRestaurant", address);
        var menu = new Menu(restaurant);
        var dish = new Dish("TestDish", 10, menu);
        _dbContext.Restaurants.Add(restaurant);
        _dbContext.Menus.Add(menu);
        _dbContext.Dishes.Add(dish);
        await _dbContext.SaveChangesAsync();
        // Act
        var result = await _controller.GetMenuAsync(restaurant.Id);
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsType<MenuResponse>(okResult.Value);
        Assert.Equal(menu.Id, model.Id);
        Assert.Equal(menu.RestaurantId, model.RestaurantId);
        Assert.Equal(dish.Id, model.Dishes[0].Id);
        Assert.Equal(dish.Name, model.Dishes[0].Name);
        Assert.Equal(dish.Price, model.Dishes[0].Price);
    }
    
    [Fact]
    public async void GetMenuShouldThrowKeyNotFoundException()
    {
        // Arrange
        var restaurant = new Restaurant("TestRestaurant", new Address("TestStreet", "TestCity", 100));
        _dbContext.Restaurants.Add(restaurant);
        await _dbContext.SaveChangesAsync();
        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetMenu(Guid.NewGuid()));
    }
    
    [Fact]
    public async void GetMenuWithRestaurantDoesNotHaveAMenuShouldThrowKeyNotFoundException()
    {
        // Arrange
        var restaurant = new Restaurant("TestRestaurant", new Address("TestStreet", "TestCity", 100));
        _dbContext.Restaurants.Add(restaurant);
        await _dbContext.SaveChangesAsync();
        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetMenu(restaurant.Id));
    }

}