namespace Catalog.API.DTO;

public class DishRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public DishRequest(Guid id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }
}