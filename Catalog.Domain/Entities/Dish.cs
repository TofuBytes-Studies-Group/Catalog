namespace Catalog.Domain.Entities;

public class Dish : IDish
{
    public Dish(string name, int price, Menu menu)
    {
        Name = name;
        Price = price;
        MenuId = menu.Id;
        Menu = menu;
    }

    public Dish(){
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public Guid MenuId { get; set; }
    public Menu Menu { get; set; }
}