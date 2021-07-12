namespace ConcreteProducts.Web.Models.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ConcreteProducts.Web.Data.Models.Enumerations;

    using static Data.DataConstants;

    public class AddProductFormModel
    {
        [Required]
        [StringLength(
            ProductNameMaxLength,
            MinimumLength = ProductNameMinLength,
            ErrorMessage = DefaultNameErrorMessage)]
        public string Name { get; init; }

        [Range(
            QuantityInPalletInUnitMeasurementMinValue,
            QuantityInPalletInUnitMeasurementMaxValue,
            ErrorMessage = QuantityErrorMessage)]
        [Display(Name = "Quantity in pallet in unit of measurement")]
        public double QuantityInPalletInUnitOfMeasurement { get; init; }

        [Range(
            QuantityInPalletInPiecesMinValue,
            QuantityInPalletInPiecesMaxValue,
            ErrorMessage = QuantityErrorMessage)]
        [Display(Name = "Quantity in pallet in pieces")]
        public double QuantityInPalletInPieces { get; init; }

        [Range(
            CountInUnitMeasurementMinValue,
            CountInUnitMeasurementMaxValue,
            ErrorMessage = QuantityErrorMessage)]
        [Display(Name = "Count in unit of measurement")]
        public double CountInUnitOfMeasurement { get; init; }

        [Range(
            WeightMinValue,
            WeightMaxValue,
            ErrorMessage = QuantityErrorMessage)]
        public double Weight { get; init; }

        [EnumDataType(typeof(UnitOfMeasurement))]
        public UnitOfMeasurement UnitOfMeasurement { get; init; }

        [Display(Name = "Image URL")]
        public string ImageUrl { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        [Display(Name = "Color")]
        public int ColorId { get; init; }

        public IEnumerable<ProductCategoryViewModel> Categories { get; set; }

        public IEnumerable<ProductColorViewModel> Colors { get; set; }
    }
}
