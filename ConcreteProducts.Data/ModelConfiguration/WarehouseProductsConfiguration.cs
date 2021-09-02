namespace ConcreteProducts.Data.ModelConfiguration
{
    using ConcreteProducts.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
