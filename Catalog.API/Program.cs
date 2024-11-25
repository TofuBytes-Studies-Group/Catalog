using Catalog.API.DTO;
using Catalog.API.Kafka;
using Catalog.API.Services;
using Catalog.API.Validators;
using Catalog.Infrastructure;
using Catalog.Infrastructure.Kafka;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CatalogContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("CatalogDatabase")));
// Add the producer service as singletons:
builder.Services.AddSingleton<KafkaProducer>();
// Add the kafka consumer service as a hosted service (background service that runs for the lifetime of the application):
builder.Services.AddHostedService<KafkaConsumer>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IValidator<RestaurantRequest>, RestaurantRequestValidator>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
