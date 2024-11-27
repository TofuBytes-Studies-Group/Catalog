namespace Catalog.API.DTO;

public class PocOrderDTO
{
    public Guid Restaurant { get; set; }
    public List<Guid> Dishes { get; set; }
    
    public PocOrderDTO(Guid restaurant, List<Guid> dishes)
    {
        Restaurant = restaurant;
        Dishes = dishes;
    }
   
    public PocOrderDTO()
    {
    }
}