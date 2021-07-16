namespace ConcreteProducts.Web.Data.ModelConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using ConcreteProducts.Web.Data.Models;

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .HasOne(c => c.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(c => c.Warehouse)
                .WithMany(p => p.Products)
                .HasForeignKey(c => c.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
