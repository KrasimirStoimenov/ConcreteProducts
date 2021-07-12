namespace ConcreteProducts.Web.Models.Categories
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class AddCategoryFormModel
    {
        [Required]
        [StringLength(
            CategoryNameMaxLength,
            MinimumLength = CategoryNameMinLength,
            ErrorMessage = DefaultNameErrorMessage)]
        [Display(Name = "Category name")]
        public string Name { get; init; }
    }
}
