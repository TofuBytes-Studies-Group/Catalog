using Moq;
using Microsoft.AspNetCore.Mvc;
using Catalog.API.Controllers;
using Catalog.API.Services;
using Catalog.API.DTO;
using FluentValidation;
using FluentValidation.Results;

namespace Catalog.UnitTests
{
    public class RestaurantControllerTest
    {
        private readonly Mock<IRestaurantService> _mockService;
        private readonly RestaurantController _controller;
        private readonly Mock<IValidator<RestaurantRequest>> _mockValidator;

        public RestaurantControllerTest()
        {
            _mockService = new Mock<IRestaurantService>();
            _mockValidator = new Mock<IValidator<RestaurantRequest>>();
            _controller = new RestaurantController(_mockService.Object, _mockValidator.Object);
        }

        [Fact]
        public async void GetRestaurantsShouldReturnOkResultWithListOfRestaurants()
        {
            // Arrange
            var offset = 0;
            var limit = 10;
            var search = "restaurant";
            var restaurants = new List<RestaurantResponse>
            {
                new RestaurantResponse( Guid.NewGuid(), "Restaurant 1", null ),
                new RestaurantResponse( Guid.NewGuid(), "Restaurant 2", null)
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
        [InlineData(-1)]
        public async void GetRestaurantsShouldReturnBadRequestWhenLimitIsInvalid(int limit)
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

        [Fact]
        public async void CreateRestaurantShouldReturnOkResultWithRestaurant()
        {
           
            // Arrange
            var restaurantRequest = new RestaurantRequest ("Restaurant 1", Guid.NewGuid());
            var validationResult = new ValidationResult();
            _mockValidator.Setup(v => v.ValidateAsync(restaurantRequest, default)).ReturnsAsync(validationResult);
            var restaurantResponse = new RestaurantResponse(Guid.NewGuid(), restaurantRequest.Name, null);
            _mockService.Setup(s => s.CreateRestaurant(restaurantRequest)).ReturnsAsync(restaurantResponse);

            // Act
            var result = await _controller.CreateRestaurant(restaurantRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsAssignableFrom<RestaurantResponse>(okResult.Value);
            Assert.Equal(restaurantRequest.Name, response.Name);
        }
        
    
    }
}