namespace ConcreteProducts.Web.Models.Products
{
    using System.ComponentModel.DataAnnotations;
    using ConcreteProducts.Web.Data.Models.Enumerations;

    public class ProductInListViewModel
    {
        public int Id { get; init; }

        [Display(Name = "Product name")]
        public string Name { get; init; }

        [Display(Name = "Quantity in pallet in unit of measurement")]
        public double QuantityInPalletInUnitOfMeasurement { get; init; }

        [Display(Name = "Quantity in pallet in pieces")]
        public double QuantityInPalletInPieces { get; init; }

        [EnumDataType(typeof(UnitOfMeasurement))]
        public UnitOfMeasurement UnitOfMeasurement { get; init; }

        [Display(Name = "Image URL")]
        public string ImageUrl { get; init; }
    }
}
