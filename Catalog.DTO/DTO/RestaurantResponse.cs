namespace Catalog.DTO.DTO;

public class RestaurantResponse
{
    public Guid Id { get; }
    public string Name { get; }
    public AddressResponse? Address { get; }
    
    public RestaurantResponse(Guid id, string name, AddressResponse? address)
    {
        Id = id;
        Name = name;
        Address = address;
    }
}