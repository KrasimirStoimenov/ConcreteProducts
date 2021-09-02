namespace ConcreteProducts.Web.Areas.Admin.Models.Colors
{
    using System.ComponentModel.DataAnnotations;

    using ConcreteProducts.Web.Infrastructure.ValidationAttributes;

    using static ConcreteProducts.Common.DataAttributeConstants.Color;
    using static ConcreteProducts.Common.DataAttributeConstants.ErrorMessages;

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
