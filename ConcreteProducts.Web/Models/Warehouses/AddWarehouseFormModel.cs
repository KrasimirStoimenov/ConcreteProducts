namespace ConcreteProducts.Web.Models.Warehouses
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Warehouse;
    using static Data.DataConstants.ErrorMessages;

    public class AddWarehouseFormModel
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
