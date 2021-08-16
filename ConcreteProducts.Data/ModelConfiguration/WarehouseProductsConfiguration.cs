namespace ConcreteProducts.Data.ModelConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using ConcreteProducts.Data.Models;

    public class WarehouseProductsConfiguration : IEntityTypeConfiguration<WarehouseProductColors>
    {
        public void Configure(EntityTypeBuilder<WarehouseProductColors> builder)
        {
            builder
                .HasKey(wp => new { wp.WarehouseId, wp.ProductColorId });

            builder
                .HasOne(w => w.Warehouse)
                .WithMany(wp => wp.WarehouseProducts)
                .HasForeignKey(w => w.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(p => p.ProductColor)
                .WithMany(pw => pw.Warehouses)
                .HasForeignKey(p => p.ProductColorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
