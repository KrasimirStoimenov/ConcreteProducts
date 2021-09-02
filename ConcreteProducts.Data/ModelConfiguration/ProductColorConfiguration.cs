namespace ConcreteProducts.Data.ModelConfiguration
{
    using ConcreteProducts.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProductColorConfiguration : IEntityTypeConfiguration<ProductColor>
    {
        public void Configure(EntityTypeBuilder<ProductColor> builder)
        {
            builder
                .HasKey(c => c.ProductColorId);

            builder
                .HasOne(c => c.Color)
                .WithMany(pc => pc.ProductColors)
                .HasForeignKey(c => c.ColorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(p => p.Product)
                .WithMany(pc => pc.ProductColors)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
