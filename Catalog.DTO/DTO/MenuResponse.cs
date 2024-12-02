namespace Catalog.API.DTO;
public class MenuResponse
{
    public Guid Id { get; set; }
    public Guid RestaurantId { get; set; }
    public List<DishResponse> Dishes { get; set; }
    
    public MenuResponse(Guid id, Guid restaurantId, List<DishResponse> dishes)
    {
        this.Id = id;
        this.RestaurantId = restaurantId;
        this.Dishes = dishes;
    }
}