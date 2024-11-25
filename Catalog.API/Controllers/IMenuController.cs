using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

public interface IMenuController
{
    Task<IActionResult> GetMenuAsync(Guid restaurantId);
}