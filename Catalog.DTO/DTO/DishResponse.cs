namespace Catalog.DTO.DTO;

public class DishResponse(Guid id, string name, int price)
{
    public Guid Id { get; } = id;
    public string Name { get; } = name;
    public int Price { get; } = price;
}