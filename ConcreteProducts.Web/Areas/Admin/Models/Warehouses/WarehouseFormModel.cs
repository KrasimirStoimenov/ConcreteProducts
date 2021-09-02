namespace ConcreteProducts.Web.Areas.Admin.Models.Warehouses
{
    using System.ComponentModel.DataAnnotations;

    using static ConcreteProducts.Common.DataAttributeConstants.ErrorMessages;
    using static ConcreteProducts.Common.DataAttributeConstants.Warehouse;

    public class WarehouseFormModel
    {
        [Required]
        [StringLength(
            NameMaxLength,
            MinimumLength = NameMinLength,
            ErrorMessage = DefaultNameErrorMessage)]
        [Display(Name = "Warehouse name")]
        public string Name { get; init; }
    }
}
