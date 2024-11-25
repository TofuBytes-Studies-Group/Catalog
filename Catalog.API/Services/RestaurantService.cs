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
        return new RestaurantResponse(response.Entity.Id, response.Entity.Name, null);
    }

    public async Task<List<RestaurantResponse>> SearchRestaurants(int offset, int limit, string? search)
    {
        IQueryable<Restaurant> query = _dbContext.Restaurants.Include(r => r.Address);

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(r => r.Name.ToLower().Contains(search.ToLower()));
        }

        var restaurants = await query.Skip(offset)
            .Take(limit)
            .ToListAsync();

        return restaurants.Select(r => new RestaurantResponse(
                r.Id, 
                r.Name, 
                r.Address != null ? 
                new AddressResponse(r.Address.Id, r.Address.Street, r.Address.City, r.Address.PostalCode):
                null)
            )
            .ToList();
        
    }
}