using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {
        }

        public DbSet<Restaurant> Restaurants => Set<Restaurant>();

        public DbSet<Menu> Menus => Set<Menu>();

        public DbSet<Dish> Dishes => Set<Dish>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Restaurant>().HasKey(r => r.Id);
            modelBuilder.Entity<Menu>().HasKey(m => m.Id);
            modelBuilder.Entity<Menu>().HasMany(m => m.Dishes)
                .WithOne(d => d.Menu)
                .HasForeignKey(d => d.MenuId);
            modelBuilder.Entity<Menu>().HasOne<Restaurant>(m => m.Restaurant)
                .WithOne(r => r.Menu)
                .HasForeignKey<Menu>(m => m.RestaurantId);
            modelBuilder.Entity<Dish>().HasKey(d => d.Id);
            modelBuilder.Entity<Dish>().HasOne<Menu>(d => d.Menu)
                .WithMany(m => m.Dishes)
                .HasForeignKey(d => d.MenuId);

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.ToTable("restaurant");
                entity.Property(r => r.Id).ValueGeneratedOnAdd();
                entity.Property(r => r.Name).IsRequired();
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("menu");
                entity.Property(m => m.Id).ValueGeneratedOnAdd();
                entity.Property(m => m.RestaurantId).IsRequired();
            });

            modelBuilder.Entity<Dish>(entity =>
            {
                entity.ToTable("dish");
                entity.Property(m => m.Id).ValueGeneratedOnAdd();
                entity.Property(m => m.MenuId).IsRequired();
            });
        }
    }
}