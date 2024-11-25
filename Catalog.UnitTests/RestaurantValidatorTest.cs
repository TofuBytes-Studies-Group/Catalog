using Catalog.API.DTO;
using Catalog.API.Validators;

namespace Catalog.UnitTests;

public class RestaurantValidatorTest
{
    private readonly RestaurantRequestValidator _validator;

    public RestaurantValidatorTest()
    {
        _validator = new RestaurantRequestValidator();
    }

    [Fact]
    public void ValidateShouldReturnSuccessWhenNameIsValid()
    {
        // Arrange
        var restaurantRequest = new RestaurantRequest("Restaurant 1", Guid.NewGuid());
        // Act
        var result = _validator.Validate(restaurantRequest);
        // Assert
        Assert.True(result.IsValid);
    }
    
    [Fact]
    public void ValidateShouldReturnFailureWhenNameIsEmpty()
    {
        // Arrange
        var restaurantRequest = new RestaurantRequest("", Guid.NewGuid());
        // Act
        var result = _validator.Validate(restaurantRequest);
        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("Name is required", result.Errors[0].ErrorMessage);
    }
    
    [Fact]
    public void ValidateShouldReturnFailureWhenNameIsTooShort()
    {
        // Arrange
        var restaurantRequest = new RestaurantRequest("A", Guid.NewGuid());
        // Act
        var result = _validator.Validate(restaurantRequest);
        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("Name must be between 3 and 100 characters", result.Errors[0].ErrorMessage);
    }
    
    [Fact]
    public void ValidateShouldReturnFailureWhenNameIsTooLong()
    {
        // Arrange
        var restaurantRequest = new RestaurantRequest(new string('A', 101), Guid.NewGuid());
        // Act
        var result = _validator.Validate(restaurantRequest);
        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("Name must be between 3 and 100 characters", result.Errors[0].ErrorMessage);
    }
    
    [Fact]
    public void ValidateShouldReturnFailureWhenAddressIdIsEmpty()
    {
        // Arrange
        var restaurantRequest = new RestaurantRequest("Restaurant 1", Guid.Empty);
        // Act
        var result = _validator.Validate(restaurantRequest);
        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("Address is invalid", result.Errors[0].ErrorMessage);
    }
    
    
}