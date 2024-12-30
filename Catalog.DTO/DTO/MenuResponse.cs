namespace Catalog.DTO.DTO;
public class MenuResponse(Guid id, Guid restaurantId, List<DishResponse> dishes)
{
    public Guid Id { get; } = id;
    public Guid RestaurantId { get; } = restaurantId;
    public List<DishResponse> Dishes { get; } = dishes;
}