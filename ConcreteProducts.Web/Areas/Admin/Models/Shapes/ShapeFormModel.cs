namespace ConcreteProducts.Web.Areas.Admin.Models.Shapes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ConcreteProducts.Services.Warehouses.Models;
    using ConcreteProducts.Web.Infrastructure.ValidationAttributes;

    using static Common.DataAttributeConstants.Shape;
    using static Common.DataAttributeConstants.ErrorMessages;

    public class ShapeFormModel
    {
        [Required]
        [StringLength(
            NameMaxLength,
            MinimumLength = NameMinLength,
            ErrorMessage = DefaultNameErrorMessage)]
        [Display(Name = "Shape name")]
        public string Name { get; init; }

        [Required]
        [StringLength(
            DimensionsMaxLength,
            MinimumLength = DimensionsMinLength,
            ErrorMessage = DefaultNameErrorMessage)]
        public string Dimensions { get; init; }

        [Display(Name = "Warehouse")]
        [IsValidWarehouseId]
        public int WarehouseId { get; init; }

        public IEnumerable<WarehouseBaseServiceModel> Warehouses { get; set; }
    }
}
