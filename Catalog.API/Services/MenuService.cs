using Catalog.API.DTO;
using Catalog.Domain.Entities;
using Catalog.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Services;

public class MenuService : IMenuService
{
    private readonly CatalogContext _dbContext;
    
    public MenuService(CatalogContext context)
    {
        _dbContext = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task<MenuResponse> GetMenu(Guid restaurantId)
    {
        var response = await _dbContext.Menus
            .Where(m => m.RestaurantId == restaurantId)
            .Include(menu => menu.Dishes)
            .FirstOrDefaultAsync() ?? throw new KeyNotFoundException();
        List<DishResponse> dishes = response.Dishes
            .Select(d => new DishResponse(d.Id, d.Name, d.Price))
            .ToList();
        return new MenuResponse(response.Id, response.RestaurantId, dishes);
    }
}