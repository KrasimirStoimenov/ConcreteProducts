namespace ConcreteProducts.Web.Areas.Admin.Models.Colors
{
    using System.ComponentModel.DataAnnotations;
    using ConcreteProducts.Web.Infrastructure.ValidationAttributes;

    using static Common.DataAttributeConstants.Color;
    using static Common.DataAttributeConstants.ErrorMessages;

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
