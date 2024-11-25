using Catalog.Domain.Entities;

namespace Catalog.Domain.Interfaces;

public interface IDish
{
    Guid Id { get; set; }
    string Name { get; set; }
    int Price { get; set; }
    Guid MenuId { get; set; }
    Menu Menu { get; set; }
    
}