namespace ConcreteProducts.Web.Models.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ConcreteProducts.Web.Data.Models.Enumerations;
    using ConcreteProducts.Web.Services.Colors.Models;
    using ConcreteProducts.Web.Services.Categories.Models;
    using ConcreteProducts.Web.Services.Warehouses.Models;

    using static Data.DataConstants.Product;
    using static Data.DataConstants.ErrorMessages;

    public class AddProductFormModel
    {
        [Required]
        [StringLength(
            NameMaxLength,
            MinimumLength = NameMinLength,
            ErrorMessage = DefaultNameErrorMessage)]
        [Display(Name = "Product name")]
        public string Name { get; init; }

        [Required]
        [StringLength(
            DimensionsMaxLength,
            MinimumLength = DimensionsMinLength,
            ErrorMessage = DefaultNameErrorMessage)]
        public string Dimensions { get; init; }

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

        [EnumDataType(typeof(UnitOfMeasurement))]
        public UnitOfMeasurement UnitOfMeasurement { get; init; }

        [Range(
            WeightMinValue,
            WeightMaxValue,
            ErrorMessage = QuantityErrorMessage)]
        public double Weight { get; init; }

        [Required]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        [Display(Name = "Color")]
        public int ColorId { get; init; }

        [Display(Name = "Warehouse")]
        public int WarehouseId { get; init; }

        public IEnumerable<CategoryServiceModel> Categories { get; set; }

        public IEnumerable<ColorServiceModel> Colors { get; set; }

        public IEnumerable<WarehouseServiceModel> Warehouses { get; set; }
    }
}
