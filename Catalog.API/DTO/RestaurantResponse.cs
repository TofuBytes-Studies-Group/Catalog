namespace Catalog.API.DTO;

public class RestaurantResponse
{
    public Guid Id { get; set;}
    public string Name { get; set; }
    
    public RestaurantResponse(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
    
}