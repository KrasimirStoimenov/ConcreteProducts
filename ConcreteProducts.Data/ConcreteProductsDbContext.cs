namespace ConcreteProducts.Data
{
    using ConcreteProducts.Data.ModelConfiguration;
    using ConcreteProducts.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ConcreteProductsDbContext : IdentityDbContext
    {
        public ConcreteProductsDbContext(DbContextOptions<ConcreteProductsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; init; }

        public DbSet<Color> Colors { get; init; }

        public DbSet<Category> Categories { get; init; }

        public DbSet<ProductColor> ProductColors { get; init; }

        public DbSet<Shape> Shapes { get; init; }

        public DbSet<ShapeHistory> ShapeHistories { get; init; }

        public DbSet<Warehouse> Warehouses { get; init; }

        public DbSet<WarehouseProductColors> WarehouseProductColors { get; init; }

        public DbSet<ChatMessage> ChatMessages { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new ProductColorConfiguration());
            builder.ApplyConfiguration(new ShapeConfiguration());
            builder.ApplyConfiguration(new WarehouseProductsConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
