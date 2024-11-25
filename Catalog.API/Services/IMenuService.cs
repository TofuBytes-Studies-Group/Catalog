using Catalog.API.DTO;

namespace Catalog.API.Services;

public interface IMenuService
{
    Task<MenuResponse> GetMenu(Guid restaurantId);
}