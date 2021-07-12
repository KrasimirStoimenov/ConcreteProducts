namespace ConcreteProducts.Web.Models.Colors
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class AddColorFormModel
    {
        [Required]
        [StringLength(
            ColorNameMaxLength,
            MinimumLength = ColorNameMinLength,
            ErrorMessage = DefaultNameErrorMessage)]
        [Display(Name = "Color name")]
        public string Name { get; init; }
    }
}
