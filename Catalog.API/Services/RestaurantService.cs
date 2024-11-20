using Catalog.API.DTO;
using Catalog.Domain.Entities;
using Catalog.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Services;

public class RestaurantService : IRestaurantService
{
    private readonly CatalogContext _dbContext;

    public RestaurantService(CatalogContext context)
    {
        _dbContext = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<RestaurantResponse> CreateRestaurant(RestaurantRequest restaurantRequest)
    {
        var restaurant = new Restaurant
        {
            Name = restaurantRequest.Name
        };
        var response = await _dbContext.Restaurants.AddAsync(restaurant);
        await _dbContext.SaveChangesAsync();
        return new RestaurantResponse(response.Entity.Id, response.Entity.Name);
    }

    public async Task<List<RestaurantResponse>> SearchRestaurants(int offset, int limit, string? search)
    {
        List<Restaurant> restaurants;
        if (search != null)
        {   
            search = search.ToLower();
            restaurants = await _dbContext.Restaurants.Where(r => r.Name.ToLower().Contains(search))
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
        }
        else
        {
            restaurants = await _dbContext.Restaurants.Skip(offset)
                .Take(limit)
                .ToListAsync();
        }

        return restaurants.Select(r => new RestaurantResponse(r.Id, r.Name)).ToList();
    }
}