namespace Catalog.DTO.DTO;

public class CatalogRequest(Guid restaurantId, Guid customerId, string customerUsername, List<Guid> dishes)
{
    public Guid RestaurantId { get; } = restaurantId;
    public Guid CustomerId { get; } = customerId;
    public string CustomerUsername { get; } = customerUsername;
    public List<Guid> Dishes { get; } = dishes;
}