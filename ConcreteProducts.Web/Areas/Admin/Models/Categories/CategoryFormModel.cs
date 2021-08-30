namespace ConcreteProducts.Web.Areas.Admin.Models.Categories
{
    using System.ComponentModel.DataAnnotations;
    using ConcreteProducts.Web.Infrastructure.ValidationAttributes;

    using static Common.DataAttributeConstants.Category;
    using static Common.DataAttributeConstants.ErrorMessages;

    public class CategoryFormModel
    {
        public int Id { get; init; }

        [Required]
        [StringLength(
            NameMaxLength,
            MinimumLength = NameMinLength,
            ErrorMessage = DefaultNameErrorMessage)]
        [Display(Name = "Category name")]
        public string Name { get; init; }
    }
}
