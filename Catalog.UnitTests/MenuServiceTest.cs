using Catalog.API.Services;
using Catalog.Domain.Entities;
using Catalog.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Catalog.UnitTests;

public class MenuServiceTest : IDisposable
{
    private readonly CatalogContext _dbContext;
    private readonly MenuService _service;
    
    public MenuServiceTest()
    {
        var options = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: "MenuDbTest")
            .Options;
        _dbContext = new CatalogContext(options);
        _service = new MenuService(_dbContext);
    }
    
    public void Dispose()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }
    
    [Fact]
    public async void GetMenuInDbShouldReturnMenuWithDishes()
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
        var result = await _service.GetMenu(restaurant.Id);
        // Assert
        Assert.Equal(menu.Id, result.Id);
        Assert.Equal(menu.RestaurantId, result.RestaurantId);
        Assert.Equal(dish.Id, result.Dishes[0].Id);
        Assert.Equal(dish.Name, result.Dishes[0].Name);
        Assert.Equal(dish.Price, result.Dishes[0].Price);
    }
    
    [Fact]
    public async void GetMenuInDbShouldThrowKeyNotFoundException()
    {
        // Arrange
        var restaurant = new Restaurant("TestRestaurant", new Address("TestStreet", "TestCity", 100));
        _dbContext.Restaurants.Add(restaurant);
        await _dbContext.SaveChangesAsync();
        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetMenu(Guid.NewGuid()));
    }
    
}