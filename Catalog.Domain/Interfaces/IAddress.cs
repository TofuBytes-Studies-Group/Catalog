using Catalog.Domain.Entities;

namespace Catalog.Domain.Interfaces;

public interface IAddress
{
    Guid Id { get; set; }
    string Street { get; set; }
    string City { get; set; }
    int PostalCode { get; set; }
    Restaurant Restaurant { get; set; }
}