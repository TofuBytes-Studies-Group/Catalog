using Catalog.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[ApiController]
[Route("/menu")]
public class MenuController : ControllerBase, IMenuController
{
    private readonly IMenuService _service;
    
    public MenuController(IMenuService service)
    {
        _service = service;
    }
    
    [HttpGet]
    [Route("{restaurantId}")]
    public async Task<IActionResult> GetMenuAsync(Guid restaurantId)
    {
        try
        {
            var menu = await _service.GetMenu(restaurantId);
            return Ok(menu);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("The given restaurant might not exist or it does not have a menu");
        }
    }
}