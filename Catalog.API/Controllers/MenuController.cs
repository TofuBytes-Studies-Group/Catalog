using Catalog.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[ApiController]
[Route("/menu")]
public class MenuController(IMenuService service) : ControllerBase, IMenuController
{
    [HttpGet]
    [Route("{restaurantId:guid}")]
    public async Task<IActionResult> GetMenuAsync(Guid restaurantId)
    {
        try
        {
            var menu = await service.GetMenu(restaurantId);
            return Ok(menu);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("The given restaurant might not exist or it does not have a menu");
        }
    }
}