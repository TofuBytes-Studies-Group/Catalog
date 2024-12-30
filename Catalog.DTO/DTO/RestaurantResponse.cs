namespace Catalog.DTO.DTO;

public class RestaurantResponse(Guid id, string name, AddressResponse? address)
{
    public Guid Id { get; } = id;
    public string Name { get; } = name;
    public AddressResponse? Address { get; } = address;
}