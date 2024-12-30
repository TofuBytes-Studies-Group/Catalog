using Catalog.DTO.DTO;

namespace Catalog.API.Services;

public interface IPocOrderService
{
    public Task CreateOrder(CatalogResponse order);
}