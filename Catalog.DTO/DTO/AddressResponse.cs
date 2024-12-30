namespace Catalog.DTO.DTO;

public class AddressResponse
{
    public Guid Id { get; }
    public string Street { get; }
    public string City { get; }
    public int PostalCode { get; }
    
    public AddressResponse(Guid id, string street, string city, int postalCode)
    {
        Id = id;
        Street = street;
        City = city;
        PostalCode = postalCode;
    }
}