namespace ConcreteProducts.Web.Models.Colors
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Color;
    using static Data.DataConstants.ErrorMessages;

    public class ColorFormModel
    {
        [Required]
        [StringLength(
            NameMaxLength,
            MinimumLength = NameMinLength,
            ErrorMessage = DefaultNameErrorMessage)]
        [Display(Name = "Color name")]
        public string Name { get; init; }
    }
}
