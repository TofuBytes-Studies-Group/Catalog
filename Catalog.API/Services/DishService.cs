using Catalog.API.DTO;
using Catalog.Infrastructure;

namespace Catalog.API.Services;

public class DishService : IDishService
{
    private readonly CatalogContext _dbContext;
    
    public DishService(CatalogContext context)
    {
        _dbContext = context ?? throw new ArgumentNullException(nameof(context));
    }
    public Task<DishRequest> GetDish(Guid dishId, Guid restaurantId)
    {
        // need to get dish by dish id the dish needs to be in the menu of the restaurant
        var response = _dbContext.Dishes
            .Where(d => d.Id == dishId && d.Menu.RestaurantId == restaurantId)
            .Select(d => new DishRequest(d.Id, d.Name, d.Price))
            .FirstOrDefault() ?? throw new KeyNotFoundException();
        return Task.FromResult(response);
    }
}