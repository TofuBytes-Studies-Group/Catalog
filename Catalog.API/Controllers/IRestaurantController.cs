using Catalog.API.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

public interface IRestaurantController 
{
    Task<IActionResult> GetRestaurants(int offset, int limit, string? search);
    Task<IActionResult> CreateRestaurant(RestaurantRequest restaurantRequest);
    
}