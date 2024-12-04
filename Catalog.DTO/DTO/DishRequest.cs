namespace Catalog.API.DTO;

public class DishRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }

    public DishRequest(Guid id, string name, int price)
    {
        Id = id;
        Name = name;
        Price = price;
    }
}