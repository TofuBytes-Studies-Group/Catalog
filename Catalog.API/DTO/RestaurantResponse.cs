
namespace Catalog.API.DTO;

public class RestaurantResponse
{
    public Guid Id { get; set;}
    public string Name { get; set; }
    public AddressResponse? Address { get; set; }
    
    public RestaurantResponse(Guid id, string name, AddressResponse? address)
    {
        Id = id;
        Name = name;
        Address = address;
    }
}