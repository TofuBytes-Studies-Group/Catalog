namespace Catalog.API.DTO;

public class DishResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    
    public DishResponse(Guid id, string name, int price)
    {
        this.Id = id;
        this.Name = name;
        this.Price = price;
    }
}