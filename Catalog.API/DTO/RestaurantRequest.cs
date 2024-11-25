using FluentValidation;

namespace Catalog.API.DTO;

public class RestaurantRequest
{
    public string Name { get; set; }
    public Guid AddressId { get; set; }

    public RestaurantRequest(string name, Guid addressId)
    {
        Name = name;
        AddressId = addressId;
    }
}

