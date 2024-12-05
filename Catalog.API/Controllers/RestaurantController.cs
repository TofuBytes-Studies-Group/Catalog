using Catalog.API.DTO;
using Catalog.API.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[ApiController]
[Route("/restaurant")]
public class RestaurantController: ControllerBase, IRestaurantController
{
    private readonly IRestaurantService _service;
    private readonly IValidator<RestaurantRequest> _validator;

    public RestaurantController(IRestaurantService service, IValidator<RestaurantRequest> validator)
    {
        _service = service;
        _validator = validator;
    }
    

    [HttpGet("list")]
    public async Task<IActionResult> GetRestaurants(int offset = 0, int limit = 20, string? search = null)
    {
        if (limit < 1 || limit > 100)
        {
            return base.BadRequest("Limit must be between 1 and 100");
        }
        var restaurants = await _service.SearchRestaurants(offset, limit, search);
        return Ok(restaurants);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateRestaurant(RestaurantRequest restaurantRequest)
    {
        var validation = await _validator.ValidateAsync(restaurantRequest);
        if (!validation.IsValid)
        {
            return BadRequest(validation.Errors);
        }
        var response = await _service.CreateRestaurant(restaurantRequest);
        return Ok(response);
        
    }
}