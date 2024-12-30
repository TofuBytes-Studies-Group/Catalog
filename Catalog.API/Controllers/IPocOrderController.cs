using Catalog.DTO.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

public interface IPocOrderController
{
    public Task<IActionResult> CreateOrderAsync(CatalogRequest orderRequest);
}