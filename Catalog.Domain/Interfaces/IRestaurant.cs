using Catalog.Domain.Entities;

namespace Catalog.Domain.Interfaces;

public interface IRestaurant
{
    Guid Id { get; set; }
    string Name { get; set; }
    Menu Menu { get; set; }
    Address Address { get; set; }
    Guid AddressId { get; set; }
}