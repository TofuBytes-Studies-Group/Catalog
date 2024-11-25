using Catalog.API.DTO;
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
        public async void CreateRestaurantInDbShouldReturnRestaurant()
        {
            // Arrange
            var restaurant = new RestaurantRequest("TestRestaurant", Guid.NewGuid());
            // Act
            var result = await _service.CreateRestaurant(restaurant);
            // Assert
            Assert.Equal(restaurant.Name, result.Name);
        }

        [Fact]
        public async void GetRestaurantsWithLimitShouldReturnLimitsAmountRestaurant()
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
            var result = await _service.SearchRestaurants(0, limit, null);
            // Assert
            Assert.Equal(limit, result.Count);
        }

        [Fact]
        public async void
            GetRestaurantsWithLimitWhereNumberOfRestaurantsIsLessThanLimitShouldReturnAllRemainingRestaurants()
        {
            // Arrange
            int limit = 20;
            for (int i = 0; i <= 10; i++)
            {
                var address = new Address("TestStreet" + i , "TestCity", 100);
                string name = "TestRestaurant" + i;
                var restaurant = new Restaurant(name, address);
                _dbContext.Restaurants.Add(restaurant);
            }

            await _dbContext.SaveChangesAsync();
            // Act
            var result = await _service.SearchRestaurants(0, limit, null);
            // Assert
            Assert.NotEmpty(result);
            Assert.True(limit > result.Count);
        }

        [Fact]
        public async void GetRestaurantsWithOffsetShouldReturnRestaurantsStartingFromOffset()
        {
            // Arrange
            int offset = 5;
            int limit = 1;
            for (int i = 0; i <= 20; i++)
            {
                var address = new Address("TestStreet" + i , "TestCity", 100);
                string name = "TestRestaurant" + i;
                var restaurant = new Restaurant(name, address);
                _dbContext.Restaurants.Add(restaurant);
            }

            await _dbContext.SaveChangesAsync();
            // Act
            var result = await _service.SearchRestaurants(offset, limit, null);
            // Assert

            Assert.Equal("TestRestaurant" + offset, result[0].Name);
        }

        [Fact]
        public async void GetRestaurantWithSearchShouldReturnRestaurantWithContainingSearchString()
        {
            // Arrange
            
            var restaurant = new Restaurant("TestRestaurant", new Address("TestStreet1", "TestCity", 100));
            _dbContext.Restaurants.Add(restaurant);
            var restaurant2 = new Restaurant("TestRestaurant2", new Address("TestStreet2", "TestCity", 100));
            _dbContext.Restaurants.Add(restaurant2);

            await _dbContext.SaveChangesAsync();
            // Act
            var result = await _service.SearchRestaurants(0, 10, "TestRestaurant2");
            // Assert
            Assert.Single(result);
            Assert.Equal("TestRestaurant2", result[0].Name);
        }

        [Fact]
        public async void GetRestaurantsWithSearchShouldReturnRestaurantsContainingSearchString()
        {
            var restaurant = new Restaurant("TestRestaurant", new Address("TestStreet1", "TestCity", 100));
            _dbContext.Restaurants.Add(restaurant);
            var restaurant2 = new Restaurant("TestRestaurant2", new Address("TestStreet2", "TestCity", 100));
            _dbContext.Restaurants.Add(restaurant2);
            var restaurant3 = new Restaurant("mcDonald's", new Address("TestStreet3", "TestCity", 100));
            _dbContext.Restaurants.Add(restaurant3);

            await _dbContext.SaveChangesAsync();
            // Act
            var result = await _service.SearchRestaurants(0, 10, "Test");
            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("TestRestaurant", result[0].Name);
        }

        [Fact]
        public async void GetRestaurantsWithSearchShouldReturnRestaurantsContainingSearchStringCaseInsensitive()
        {
            var restaurant = new Restaurant("TestRestaurant", new Address("TestStreet1", "TestCity", 100));
            _dbContext.Restaurants.Add(restaurant);
            var restaurant2 = new Restaurant("TestRestaurant2", new Address("TestStreet2", "TestCity", 100));
            _dbContext.Restaurants.Add(restaurant2);
            var restaurant3 = new Restaurant("mcDonald's", new Address("TestStreet3", "TestCity", 100));
            _dbContext.Restaurants.Add(restaurant3);

            await _dbContext.SaveChangesAsync();
            // Act
            var result = await _service.SearchRestaurants(0, 10, "restaurant");
            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetRestaurantsReturnRestaurantsWithAddressResponse()
        {
            var address = new Address("TestStreet", "TestCity", 100);
            var restaurant = new Restaurant("TestRestaurant", address);
            _dbContext.Restaurants.Add(restaurant);
            await _dbContext.SaveChangesAsync();
            // Act
            var result = await _service.SearchRestaurants(0, 10, null);
            // Assert
            Assert.NotNull(result[0].Address);
            Assert.Equal(address.Street, result[0].Address?.Street);
        }
    }
}