namespace ConcreteProducts.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ConcreteProducts.Data.Models.Enumerations;

    using static ConcreteProducts.Common.DataAttributeConstants.Product;

    public class Product
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DimensionsMaxLength)]
        public string Dimensions { get; set; }

        public double QuantityInPalletInUnitOfMeasurement { get; set; }

        public double QuantityInPalletInPieces { get; set; }

        public double CountInUnitOfMeasurement { get; set; }

        public double Weight { get; set; }

        public UnitOfMeasurement UnitOfMeasurement { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; init; }

        public ICollection<ProductColor> ProductColors { get; init; } = new HashSet<ProductColor>();
    }
}
