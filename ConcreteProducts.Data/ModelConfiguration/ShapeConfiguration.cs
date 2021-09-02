namespace ConcreteProducts.Data.ModelConfiguration
{
    using ConcreteProducts.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ShapeConfiguration : IEntityTypeConfiguration<Shape>
    {
        public void Configure(EntityTypeBuilder<Shape> builder)
        {
            builder
                .HasOne(w => w.Warehouse)
                .WithMany(s => s.Shapes)
                .HasForeignKey(w => w.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
