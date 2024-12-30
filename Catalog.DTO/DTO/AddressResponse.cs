namespace Catalog.DTO.DTO;

public class AddressResponse(Guid id, string street, string city, int postalCode)
{
    public Guid Id { get; } = id;
    public string Street { get; } = street;
    public string City { get; } = city;
    public int PostalCode { get; } = postalCode;
}