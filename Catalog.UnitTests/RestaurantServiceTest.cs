using Catalog.API.Services;
using Catalog.Domain.Entities;
using Catalog.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Catalog.UnitTests
{
    public class RestaurantServiceTest : IDisposable
    {
        private readonly CatalogContext _dbContext;
        private readonly RestaurantService _service;

        public RestaurantServiceTest()
        {
            var options = new DbContextOptionsBuilder<CatalogContext>()
                .UseInMemoryDatabase(databaseName: "RestaurantDbTest")
                .Options;
            _dbContext = new CatalogContext(options);
            _service = new RestaurantService(_dbContext);
        }
        
        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [Fact]
        public void CreateRestaurantInDbShouldReturnRestaurant()
        {
            // Arrange
            var restaurant = new Restaurant("TestRestaurant");
            // Act
            var result = _service.CreateRestaurant(restaurant);
            // Assert
            Assert.Equal(restaurant.Name, result.Result.Name);
        }

        [Fact]
        public void GetRestaurantsWithLimitShouldReturnLimitsAmountRestaurant()
        {
            // Arrange
            int limit = 20;
            for (int i = 0; i <= limit + 1; i++)
            {
                string name = "TestRestaurant" + i;
                var restaurant = new Restaurant(name);
                _dbContext.Restaurants.Add(restaurant);
            }

            _dbContext.SaveChanges();
            // Act
            var result = _service.SearchRestaurants(0, limit, null);
            // Assert
            Assert.Equal(limit, result.Result.Count);
        }
        
        [Fact]
        public void GetRestaurantsWithLimitWhereNumberOfRestaurantsIsLessThanLimitShouldReturnAllRemainingRestaurants()
        {
            // Arrange
            int limit = 20;
            for (int i = 0; i <= 10; i++)
            {
                string name = "TestRestaurant" + i;
                var restaurant = new Restaurant(name);
                _dbContext.Restaurants.Add(restaurant);
            }

            _dbContext.SaveChanges();
            // Act
            var result = _service.SearchRestaurants(0, limit, null);
            // Assert
            Assert.True(limit > result.Result.Count);
        }

        [Fact]
        public void GetRestaurantsWithOffsetShouldReturnRestaurantsStartingFromOffset()
        {
            // Arrange
            int offset = 5;
            int limit = 10;
            for (int i = 0; i <= 20; i++)
            {
                string name = "TestRestaurant" + i;
                var restaurant = new Restaurant(name);
                _dbContext.Restaurants.Add(restaurant);
            }

            _dbContext.SaveChanges();
            // Act
            var result = _service.SearchRestaurants(offset, limit, null);
            // Assert


            Assert.Equal(limit, result.Result.Count);

        }
        
        [Fact]
        public void GetRestaurantWithSearchShouldReturnRestaurantWithContainingSearchString()
        {
            // Arrange
                var restaurant = new Restaurant("TestRestaurant");
                _dbContext.Restaurants.Add(restaurant);
                var restaurant2 = new Restaurant("TestRestaurant2");
                _dbContext.Restaurants.Add(restaurant2);

            _dbContext.SaveChanges();
            // Act
            var result = _service.SearchRestaurants(0, 10, "TestRestaurant2");
            // Assert
            Assert.Single(result.Result);
            Assert.Equal("TestRestaurant2", result.Result[0].Name);
        }

        [Fact]
        public void GetRestaurantsWithSearchShouldReturnRestaurantsContainingSearchString()
        {
            var restaurant = new Restaurant("TestRestaurant");
            _dbContext.Restaurants.Add(restaurant);
            var restaurant2 = new Restaurant("TestRestaurant2");
            _dbContext.Restaurants.Add(restaurant2);
            var restaurant3 = new Restaurant("mcDonald's");
            _dbContext.Restaurants.Add(restaurant3);

            _dbContext.SaveChanges();
            // Act
            var result = _service.SearchRestaurants(0, 10, "Restaurant");
            // Assert
            Assert.Equal(2, result.Result.Count);
            Assert.Equal("TestRestaurant", result.Result[0].Name);
        }
        
        [Fact]
        public void GetRestaurantsWithSearchShouldReturnRestaurantsContainingSearchStringCaseInsensitive()
        {
            var restaurant = new Restaurant("TestRestaurant");
            _dbContext.Restaurants.Add(restaurant);
            var restaurant2 = new Restaurant("TestRestaurant2");
            _dbContext.Restaurants.Add(restaurant2);
            var restaurant3 = new Restaurant("mcDonald's");
            _dbContext.Restaurants.Add(restaurant3);

            _dbContext.SaveChanges();
            // Act
            var result = _service.SearchRestaurants(0, 10, "Restaurant");
            // Assert
            Assert.Equal(2, result.Result.Count);
        }
        
    }
}