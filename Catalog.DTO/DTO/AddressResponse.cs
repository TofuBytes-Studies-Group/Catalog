namespace Catalog.API.DTO;

public class AddressResponse
{
    public Guid Id { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public int PostalCode { get; set; }
    
    public AddressResponse(Guid id, string street, string city, int postalCode)
    {
        Id = id;
        Street = street;
        City = city;
        PostalCode = postalCode;
    }
}