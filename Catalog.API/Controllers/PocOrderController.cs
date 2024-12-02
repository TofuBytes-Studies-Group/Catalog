using Catalog.API.DTO;
using Catalog.API.Services;
using Catalog.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[ApiController]
[Route("/order")]
public class PocOrderController : ControllerBase, IPocOrderController
{
    private readonly IPocOrderService _orderService;
    private readonly IDishService _dishService;

    public PocOrderController(IPocOrderService orderService, IDishService dishService)
    {
        _orderService = orderService;
        _dishService = dishService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrderAsync(CatalogRequest orderRequest)
    {
        var dishes = new List<DishRequest>();

        foreach (var dish in orderRequest.Dishes)
        {
            try
            {
                dishes.Add(await _dishService.GetDish(dish, orderRequest.RestaurantId));
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Dish not found in the menu of the restaurant");
            }
        }

        var order = new CatalogResponse(orderRequest.RestaurantId, orderRequest.CustomerId,
            orderRequest.CustomerUsername, dishes);
        await _orderService.CreateOrder(order);
        return Created();
    }
}