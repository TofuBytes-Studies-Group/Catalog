using System.Text.Json.Serialization;
using Catalog.Domain.Interfaces;

namespace Catalog.Domain.Entities;

public class Address(string street, string city, int postalCode) : IAddress
{
    public Guid Id { get; set; }
    public string Street { get; set; } = street;
    public string City { get; set; } = city;
    public int PostalCode { get; set; } = postalCode;

    [JsonIgnore]
    public Restaurant? Restaurant { get; set; }
}