namespace ConcreteProducts.Web.Areas.Admin.Models.Categories
{
    using System.ComponentModel.DataAnnotations;

    using ConcreteProducts.Web.Infrastructure.ValidationAttributes;

    using static ConcreteProducts.Common.DataAttributeConstants.Category;
    using static ConcreteProducts.Common.DataAttributeConstants.ErrorMessages;

    public class CategoryFormModel
    {
        [Required]
        [StringLength(
            NameMaxLength,
            MinimumLength = NameMinLength,
            ErrorMessage = DefaultNameErrorMessage)]
        [Display(Name = "Category name")]
        public string Name { get; init; }
    }
}
