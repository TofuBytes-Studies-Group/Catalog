using Catalog.Domain.Interfaces;

namespace Catalog.Domain.Entities
{
    public class Restaurant : IRestaurant
    {
        public Restaurant()
        {
        }
        
        public Restaurant (string name, Address address)
        {
            Name = name;
            Address = address;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Menu Menu { get; set; }
        public Address Address { get; set; }
        public Guid AddressId { get; set; }
    }
}
