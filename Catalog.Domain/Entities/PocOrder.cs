namespace Catalog.Domain.Entities;

public class PocOrder
{
    public Guid Restaurant { get; set; }
    public List<Guid> Dishes { get; set; }
    
    public PocOrder(Guid restaurant, List<Guid> dishes)
    {
        Restaurant = restaurant;
        Dishes = dishes;
    }
   
    public PocOrder()
    {
    }
    

}