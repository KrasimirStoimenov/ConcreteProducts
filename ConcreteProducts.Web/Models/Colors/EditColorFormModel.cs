namespace ConcreteProducts.Web.Models.Colors
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Color;
    using static Data.DataConstants.ErrorMessages;

    public class EditColorFormModel
    {
        [Display(Name = "Current color name")]
        public string CurrentColorName { get; set; }

        [Required]
        [StringLength(
            NameMaxLength,
            MinimumLength = NameMinLength,
            ErrorMessage = DefaultNameErrorMessage)]
        [Display(Name = "New color name")]
        public string NewColorName { get; init; }
    }
}
