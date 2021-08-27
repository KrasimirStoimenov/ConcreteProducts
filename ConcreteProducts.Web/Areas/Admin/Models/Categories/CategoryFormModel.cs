namespace ConcreteProducts.Web.Areas.Admin.Models.Categories
{
    using System.ComponentModel.DataAnnotations;

    using static Common.DataAttributeConstants.Category;
    using static Common.DataAttributeConstants.ErrorMessages;

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
