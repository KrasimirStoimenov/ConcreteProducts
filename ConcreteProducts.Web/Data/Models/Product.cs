namespace ConcreteProducts.Web.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ConcreteProducts.Web.Data.Models.Enumerations;
    using static DataConstants;

    public class Product
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(ProductNameMaxLength)]
        public string Name { get; set; }

        public double QuantityInPalletInUnitMeasurement { get; set; }

        public double QuantityInPalletInPieces { get; set; }

        public double CountInUnitMessurement { get; set; }

        public double Weight { get; set; }

        public UnitOfMeasurement UnitOfMeasurement { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; init; }

        public ICollection<ProductColor> ProductColors { get; init; } = new HashSet<ProductColor>();
    }
}
