namespace ConcreteProducts.Web.Models.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ConcreteProducts.Data.Models.Enumerations;
    using ConcreteProducts.Services.Categories.Models;
    using ConcreteProducts.Services.Colors.Models;
    using ConcreteProducts.Web.Infrastructure.ValidationAttributes;
    using Microsoft.AspNetCore.Http;

    using static ConcreteProducts.Common.DataAttributeConstants.ErrorMessages;
    using static ConcreteProducts.Common.DataAttributeConstants.Product;

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

        [IsValidCategoryId]
        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        [IsValidColorId]
        [Display(Name = "Color")]
        public int ColorId { get; init; }

        [Required]
        public IFormFile Image { get; init; }

        public IEnumerable<CategoryBaseServiceModel> Categories { get; set; }

        public IEnumerable<ColorBaseServiceModel> Colors { get; set; }
    }
}
