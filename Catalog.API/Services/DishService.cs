using Catalog.DTO.DTO;
using Catalog.Infrastructure;

namespace Catalog.API.Services;

public class DishService(CatalogContext context) : IDishService
{
    private readonly CatalogContext _dbContext = context ?? throw new ArgumentNullException(nameof(context));

    public Task<DishResponse> GetDish(Guid dishId, Guid restaurantId)
    {

        var response = _dbContext.Dishes
            .Where(d => d.Id == dishId && d.Menu.RestaurantId == restaurantId)
            .Select(d => new DishResponse(d.Id, d.Name, d.Price))
            .FirstOrDefault() ?? throw new KeyNotFoundException();
        return Task.FromResult(response);
    }
}