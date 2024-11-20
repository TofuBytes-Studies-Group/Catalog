using Catalog.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.API.DTO;

namespace Catalog.API.Services
{
    public interface IRestaurantService
    {
        Task<Restaurant> CreateRestaurant(Restaurant restaurant);
        Task<List<RestaurantResponse>> SearchRestaurants(int offset, int limit, string? search);
    }
}