using Catalog.API.Services;
using Catalog.Domain.Entities;
using Catalog.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Catalog.UnitTests;

public class DishServiceTest
{
    private readonly CatalogContext _dbContext;
    private readonly IDishService _dishService;
    
    public DishServiceTest()
    {
        _dbContext = new CatalogContext(new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase("Catalog")
            .Options);
        _dishService = new DishService(_dbContext);
    }
    
    void Dispose()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }
    
    [Fact]
    public async void GetDishShouldReturnDish()
    {
        // Arrange
        var restaurant = new Restaurant("TestRestaurant", new Address("TestStreet", "TestCity", 100));
        var menu = new Menu(restaurant);
        var dish = new Dish("TestDish", 10, menu);
        _dbContext.Restaurants.Add(restaurant);
        _dbContext.Menus.Add(menu);
        _dbContext.Dishes.Add(dish);
        await _dbContext.SaveChangesAsync();
        // Act
        var result = await _dishService.GetDish(dish.Id, restaurant.Id);
        // Assert
        Assert.Equal(dish.Name, result.Name);
    }
    
    [Fact]
    public async void GetDishShouldThrowKeyNotFoundException()
    {
        // Arrange
        var restaurant = new Restaurant("TestRestaurant", new Address("TestStreet", "TestCity", 100));
        var menu = new Menu(restaurant);
        var dish = new Dish("TestDish", 10, menu);
        _dbContext.Restaurants.Add(restaurant);
        _dbContext.Menus.Add(menu);
        _dbContext.Dishes.Add(dish);
        await _dbContext.SaveChangesAsync();
        // Act
        // Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(async () => await _dishService.GetDish(Guid.NewGuid(), restaurant.Id));
    }
    
    [Fact]
    public async void GetDishShouldThrowKeyNotFoundExceptionWhenDishIsNotInMenu()
    {
        // Arrange
        var restaurant = new Restaurant("TestRestaurant", new Address("TestStreet", "TestCity", 100));
        var menu = new Menu(restaurant);
        var dish = new Dish("TestDish", 10, menu);
        _dbContext.Restaurants.Add(restaurant);
        _dbContext.Menus.Add(menu);
        _dbContext.Dishes.Add(dish);
        await _dbContext.SaveChangesAsync();
        // Act
        // Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(async () => await _dishService.GetDish(dish.Id, Guid.NewGuid()));
    }
    
}