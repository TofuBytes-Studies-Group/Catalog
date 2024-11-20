using Catalog.API.Services;
using Catalog.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[ApiController]
[Route("/restaurant")]
public class RestaurantController: ControllerBase, IRestaurantController
{
    private readonly IRestaurantService _service;

    public RestaurantController(IRestaurantService service)
    {
        _service = service;
    }
    
    [HttpGet]
    [Route("/list")]
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
    public Task<IActionResult> CreateRestaurant(Restaurant restaurant)
    {
        throw new NotImplementedException();
    }
}