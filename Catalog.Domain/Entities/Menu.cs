namespace Catalog.Domain.Entities;

public class Menu : IMenu
{
    public Menu(Restaurant restaurant)
    {
        RestaurantId = restaurant.Id;
        Restaurant = restaurant;
        Dishes = new List<Dish>();
    }
    
    public Menu(){
    }

    public Guid Id { get; set; }
    public Guid RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }
    public List<Dish> Dishes { get; set; }
}