using Catalog.API.DTO;
using Catalog.Domain.Entities;

namespace Catalog.API.Services;

public interface IPocOrderService
{
    public Task CreateOrder(CatalogResponse order);
}