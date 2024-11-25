using Catalog.API.DTO;
using Catalog.Domain.Entities;

namespace Catalog.API.Services;

public interface IMenuService
{
    Task<MenuResponse> GetMenu(Guid restaurantId);
}