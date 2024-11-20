using Catalog.Infrastructure;
using Catalog.Infrastructure.Kafka;

namespace Catalog.API.Services
{
    public class TestService  
    {
        private readonly KafkaProducer _kafkaProducer;
        private readonly CatalogContext _dbContext;
        public TestService(KafkaProducer kafkaProducer, CatalogContext context)
        {
            _kafkaProducer = kafkaProducer;
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async void DoStuff()
        {
            var test = new Dbtest(_dbContext);
            test.CreateRestaurant();
            
            // Brug KafkaProducer
            await _kafkaProducer.ProduceAsync("topic", "Virker", "From DOSTUFF");
            
        }
    }
}
