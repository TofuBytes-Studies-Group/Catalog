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

public class RestaurantIntegrationTest : IDisposable
{
    private readonly CatalogContext _dbContext;
    private readonly RestaurantService _service;
    private readonly RestaurantController _controller;
    private readonly IValidator<RestaurantRequest> _validator;
    public RestaurantIntegrationTest()
    {
        var options = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: "RestaurantDbTest")
            .Options;
        _dbContext = new CatalogContext(options);
        _service = new RestaurantService(_dbContext);
        _validator = new RestaurantRequestValidator();
        _controller = new RestaurantController(_service, _validator);
    }
    
    public void Dispose()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }
    
    [Fact]
    public async void GetRestaurantsShouldReturnOkResultWithListOfRestaurants()
    {
        // Arrange
        int limit = 20;
        for (int i = 0; i <= limit + 1; i++)
        {
            var address = new Address("TestStreet" + i , "TestCity", 100);
            string name = "TestRestaurant" + i;
            var restaurant = new Restaurant(name, address);
            _dbContext.Restaurants.Add(restaurant);
        }
        await _dbContext.SaveChangesAsync();
        // Act
        var result = await _controller.GetRestaurants(0, limit);
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsAssignableFrom<List<RestaurantResponse>>(okResult.Value);
        Assert.Equal(limit, model.Count);
    }
    
    [Fact]
    public async void GetRestaurantsShouldReturnOkResultWithListOfRestaurantsWhenSearchIsProvided()
    {
        // Arrange
        int limit = 20;
        for (int i = 0; i <= limit + 1; i++)
        {
            var address = new Address("TestStreet" + i , "TestCity", 100);
            string name = "TestRestaurant" + i;
            var restaurant = new Restaurant(name, address);
            _dbContext.Restaurants.Add(restaurant);
        }
        await _dbContext.SaveChangesAsync();
        // Act
        var result = await _controller.GetRestaurants(0, limit, "1");
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsAssignableFrom<List<RestaurantResponse>>(okResult.Value);
        Assert.Equal(12, model.Count);
    }
    
    [Fact]
    public async void GetRestaurantsShouldReturnOkResultWithNoContentWhenNoRestaurantsFound()
    {
        // Arrange
        // Act
        var result = await _controller.GetRestaurants(0,20,"empty");
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsAssignableFrom<List<RestaurantResponse>>(okResult.Value);
        Assert.Empty(model);
    }
    
}