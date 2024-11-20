using System.Collections;
using Catalog.Domain.Entities;
using Catalog.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Services;

public class RestaurantService
{
    private readonly CatalogContext _dbContext;

    public RestaurantService(CatalogContext context)
    {
        _dbContext = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Restaurant> CreateRestaurant(Restaurant restaurant)
    {
        var response = await _dbContext.Restaurants.AddAsync(restaurant);
        await _dbContext.SaveChangesAsync();
        return new Restaurant
        {
            Id = response.Entity.Id,
            Name = response.Entity.Name
        };
    }

    public async Task<List<Restaurant>> SearchRestaurants(int offset, int limit, string? search)

    {
        List<Restaurant> restaurants;
        if(search != null){
            restaurants = await _dbContext.Restaurants.Where(r => r.Name.Contains(search)).Skip(offset).Take(limit).ToListAsync();
        }
        else{
            restaurants = await _dbContext.Restaurants.Skip(offset).Take(limit).ToListAsync();
        }
        return restaurants;
    }
    
}