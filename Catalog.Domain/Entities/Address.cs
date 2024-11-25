using System.Text.Json.Serialization;
using Catalog.Domain.Interfaces;

namespace Catalog.Domain.Entities;

public class Address : IAddress
{
    public Address(string street, string city, int postalCode)
    {
        Street = street;
        City = city;
        PostalCode = postalCode;
    }
    
    public Address()
    {
    }
    
    public Guid Id { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public int PostalCode { get; set; }
    [JsonIgnore]
    public Restaurant Restaurant { get; set; }
}