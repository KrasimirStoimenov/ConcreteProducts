namespace ConcreteProducts.Web.Models.Shape
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ConcreteProducts.Web.Models.Products;

    using static Data.DataConstants.Shape;
    using static Data.DataConstants.ErrorMessages;

    public class AddShapeFormModel
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
        public int WarehouseId { get; init; }

        public IEnumerable<ShapeWarehouseViewModel> Warehouses { get; set; }
    }
}
