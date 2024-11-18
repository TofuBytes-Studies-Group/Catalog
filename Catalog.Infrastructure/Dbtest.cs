using Catalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure;

public class Dbtest
{
    private readonly CatalogContext _dbContext;

    public Dbtest(CatalogContext context)
    {
        _dbContext = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void CreateRestaurant()
    {
        try
        {
            var menu = _dbContext.Menus.FirstOrDefault(r => r.Id == Guid.Parse("88342fcf-0d37-44b1-b030-1d562d49b44c"));
            if (menu != null)
            {
                var dish = new Dish("Bigmac", 35, menu);

                _dbContext.Dishes.Add(dish);
            }

            _dbContext.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}