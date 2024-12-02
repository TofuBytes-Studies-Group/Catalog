
using Catalog.API.DTO;

namespace Catalog.API.Services;

public interface IDishService
{
    public Task<DishRequest> GetDish(Guid dishId, Guid restaurantId);
}