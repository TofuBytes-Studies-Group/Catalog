namespace Catalog.DTO.DTO;

public class RestaurantRequest(string name, Guid addressId)
{
    public string Name { get; } = name;
    public Guid AddressId { get; } = addressId;
}

