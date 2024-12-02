namespace Catalog.API.DTO;

public class CatalogRequest

{
    public Guid RestaurantId { get; set; }
    public Guid CustomerId { get; set; }
    public string CustomerUsername { get; set; }
    public List<Guid> Dishes { get; set; }
    
    public CatalogRequest(Guid restaurantId, Guid customerId, string customerUsername, List<Guid> dishes)
    {
        RestaurantId = restaurantId;
        CustomerId = customerId;
        CustomerUsername = customerUsername;
        Dishes = dishes;
    }
   
    public CatalogRequest()
    {
    }
}