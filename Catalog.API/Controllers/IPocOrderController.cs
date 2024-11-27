using Catalog.API.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

public interface IPocOrderController
{
    Task<IActionResult> CreateOrderAsync(PocOrderDTO orderRequest);
}