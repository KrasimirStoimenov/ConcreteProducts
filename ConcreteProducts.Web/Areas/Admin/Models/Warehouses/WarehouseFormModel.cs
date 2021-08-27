namespace ConcreteProducts.Web.Areas.Admin.Models.Warehouses
{
    using System.ComponentModel.DataAnnotations;

    using static Common.DataAttributeConstants.Warehouse;
    using static Common.DataAttributeConstants.ErrorMessages;

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
