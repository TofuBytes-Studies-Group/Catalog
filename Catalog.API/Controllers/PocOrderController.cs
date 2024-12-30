using Catalog.API.Services;
using Catalog.DTO.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[ApiController]
[Route("/order")]
public class PocOrderController(IPocOrderService orderService, IDishService dishService)
    : ControllerBase, IPocOrderController
{
    [HttpPost]
    public async Task<IActionResult> CreateOrderAsync(CatalogRequest orderRequest)
    {
        var dishes = new List<DishResponse>();

        foreach (var dish in orderRequest.Dishes)
        {
            try
            {
                dishes.Add(await dishService.GetDish(dish, orderRequest.RestaurantId));
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Dish not found in the menu of the restaurant");
            }
        }

        var order = new CatalogResponse(orderRequest.RestaurantId, orderRequest.CustomerId,
            orderRequest.CustomerUsername, dishes);
        await orderService.CreateOrder(order);
        return Created();
    }
}