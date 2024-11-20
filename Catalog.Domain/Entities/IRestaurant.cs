namespace Catalog.Domain.Entities;

public interface IRestaurant
{
    Guid Id { get; set; }
    string Name { get; set; }
    Menu Menu { get; set; }
}