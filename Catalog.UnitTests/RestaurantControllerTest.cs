using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Catalog.API.Controllers;
using Catalog.API.Services;
using Catalog.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.API.DTO;

namespace Catalog.UnitTests
{
    public class RestaurantControllerTest
    {
        private readonly Mock<IRestaurantService> _mockService;
        private readonly RestaurantController _controller;

        public RestaurantControllerTest()
        {
            _mockService = new Mock<IRestaurantService>();
            _controller = new RestaurantController(_mockService.Object);
        }

        [Fact]
        public async Task GetRestaurantsShouldReturnOkResultWithListOfRestaurants()
        {
            // Arrange
            var offset = 0;
            var limit = 10;
            var search = "restaurant";
            var restaurants = new List<RestaurantResponse>
            {
                new RestaurantResponse() { Id = Guid.NewGuid(), Name = "Restaurant 1" },
                new RestaurantResponse() { Id = Guid.NewGuid(), Name = "Restaurant 2" }
            };
            _mockService.Setup(s => s.SearchRestaurants(offset, limit, search)).ReturnsAsync(restaurants);
            // Act
            var result = await _controller.GetRestaurants(offset, limit, search);
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<List<RestaurantResponse>>(okResult.Value);
            Assert.Equal(restaurants.Count, model.Count);
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(101)]
        public async Task GetRestaurantsShouldReturnBadRequestWhenLimitIsInvalid(int limit)
        {
            // Arrange
            var offset = 0;
            var search = "restaurant";
            // Act
            var result = await _controller.GetRestaurants(offset, limit, search);
            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Limit must be between 1 and 100", badRequestResult.Value);
        }
        
    
    }
}