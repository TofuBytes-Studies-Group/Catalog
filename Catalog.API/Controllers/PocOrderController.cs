using Catalog.API.DTO;
using Catalog.API.Services;
using Catalog.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;
[ApiController]
[Route("/order")]
public class PocOrderController: ControllerBase, IPocOrderController
{
    private readonly IPocOrderService _orderService;
    public PocOrderController(IPocOrderService orderService)
    {
        _orderService = orderService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateOrderAsync(PocOrderDTO orderRequest)
    {
        var order = new PocOrder(orderRequest.Restaurant, orderRequest.Dishes);
        await _orderService.CreateOrder(order);
        return Created();
        
    }
}