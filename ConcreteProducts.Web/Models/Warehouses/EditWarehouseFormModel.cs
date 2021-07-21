namespace ConcreteProducts.Web.Models.Warehouses
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Warehouse;
    using static Data.DataConstants.ErrorMessages;

    public class EditWarehouseFormModel
    {
        [Display(Name = "Current warehouse name")]
        public string CurrentWarehouseName { get; set; }

        [Required]
        [StringLength(
            NameMaxLength,
            MinimumLength = NameMinLength,
            ErrorMessage = DefaultNameErrorMessage)]
        [Display(Name = "New warehouse name")]
        public string NewWarehouseName { get; init; }
    }
}
