namespace Catalog.Domain.Entities
{
    public class Restaurant : IRestaurant
    {
        public Restaurant()
        {
        }
        public Restaurant(string name)
        {
            Name = name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Menu Menu { get; set; }
    }
}
