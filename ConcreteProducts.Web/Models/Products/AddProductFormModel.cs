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
        [Display(Name = "Product name")]
        public string Name { get; init; }

        [Required]
        [StringLength(
            ProductDimensionsMaxLength,
            MinimumLength = ProductDimensionsMinLength,
            ErrorMessage = DefaultNameErrorMessage)]
        public string Dimensions { get; init; }

        [Range(
            ProductQuantityInPalletInUnitMeasurementMinValue,
            ProductQuantityInPalletInUnitMeasurementMaxValue,
            ErrorMessage = QuantityErrorMessage)]
        [Display(Name = "Quantity in pallet in unit of measurement")]
        public double QuantityInPalletInUnitOfMeasurement { get; init; }

        [Range(
            ProductQuantityInPalletInPiecesMinValue,
            ProductQuantityInPalletInPiecesMaxValue,
            ErrorMessage = QuantityErrorMessage)]
        [Display(Name = "Quantity in pallet in pieces")]
        public double QuantityInPalletInPieces { get; init; }

        [Range(
            ProductCountInUnitMeasurementMinValue,
            ProductCountInUnitMeasurementMaxValue,
            ErrorMessage = QuantityErrorMessage)]
        [Display(Name = "Count in unit of measurement")]
        public double CountInUnitOfMeasurement { get; init; }

        [EnumDataType(typeof(UnitOfMeasurement))]
        public UnitOfMeasurement UnitOfMeasurement { get; init; }

        [Range(
            ProductWeightMinValue,
            ProductWeightMaxValue,
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

        public IEnumerable<ProductCategoryViewModel> Categories { get; set; }

        public IEnumerable<ProductColorViewModel> Colors { get; set; }

        public IEnumerable<ProductWarehouseViewModel> Warehouses { get; set; }
    }
}
