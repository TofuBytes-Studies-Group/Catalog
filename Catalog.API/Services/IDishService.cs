
using Catalog.API.DTO;

namespace Catalog.API.Services;

public interface IDishService
{
    public Task<DishResponse> GetDish(Guid dishId, Guid restaurantId);
}