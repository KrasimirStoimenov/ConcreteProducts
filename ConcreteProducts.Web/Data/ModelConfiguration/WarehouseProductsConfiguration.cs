namespace ConcreteProducts.Web.Data.ModelConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using ConcreteProducts.Web.Data.Models;

    public class WarehouseProductsConfiguration : IEntityTypeConfiguration<WarehouseProducts>
    {
        public void Configure(EntityTypeBuilder<WarehouseProducts> builder)
        {
            builder
                .HasKey(wp => new { wp.WarehouseId, wp.ProductId });

            builder
                .HasOne(w => w.Warehouse)
                .WithMany(wp => wp.WarehouseProducts)
                .HasForeignKey(w => w.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(p => p.Product)
                .WithMany(pw => pw.ProductWarehouses)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
