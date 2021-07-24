namespace ConcreteProducts.Web.Models.Categories
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Category;
    using static Data.DataConstants.ErrorMessages;

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
