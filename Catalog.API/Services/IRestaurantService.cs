using Catalog.API.DTO;


namespace Catalog.API.Services
{
    public interface IRestaurantService
    {
        Task<RestaurantResponse> CreateRestaurant(RestaurantRequest restaurantRequest);
        Task<List<RestaurantResponse>> SearchRestaurants(int offset, int limit, string? search);
    }
}