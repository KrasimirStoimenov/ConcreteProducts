namespace ConcreteProducts.Web.Data
{
    using System.Reflection.Emit;
    using ConcreteProducts.Web.Data.Models;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProductColor>()
                .HasKey(k => new { k.ProductId, k.ColorId });

            base.OnModelCreating(builder);
        }
    }
}
