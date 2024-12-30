using Catalog.API.Services;
using Catalog.DTO.DTO;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[ApiController]
[Route("/restaurant")]
public class RestaurantController(IRestaurantService service, IValidator<RestaurantRequest> validator)
    : ControllerBase, IRestaurantController
{
    [HttpGet("list")]
    public async Task<IActionResult> GetRestaurants(int offset = 0, int limit = 20, string? search = null)
    {
        if (limit is < 1 or > 100)
        {
            return base.BadRequest("Limit must be between 1 and 100");
        }
        var restaurants = await service.SearchRestaurants(offset, limit, search);
        return Ok(restaurants);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateRestaurant(RestaurantRequest restaurantRequest)
    {
        var validation = await validator.ValidateAsync(restaurantRequest);
        if (!validation.IsValid)
        {
            return BadRequest(validation.Errors);
        }
        var response = await service.CreateRestaurant(restaurantRequest);
        return Ok(response);
        
    }
}