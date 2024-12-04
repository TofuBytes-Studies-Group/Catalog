namespace Catalog.API.DTO;

public class CatalogResponse
{
    public Guid CustomerId { get; set; }
    
    public string CustomerUsername { get; set; }
    public Guid Restaurant { get; set; }
    public List<DishResponse> Dishes { get; set; }
    
    public CatalogResponse(Guid restaurantId, Guid customerId, string customerUsername, List<DishResponse> dishes)
    {
        Restaurant = restaurantId;
        CustomerId = customerId;
        CustomerUsername = customerUsername;
        Dishes = dishes;
    }
   
    public CatalogResponse()
    {
    }
    

}