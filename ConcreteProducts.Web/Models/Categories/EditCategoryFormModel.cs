namespace ConcreteProducts.Web.Models.Categories
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Category;
    using static Data.DataConstants.ErrorMessages;

    public class EditCategoryFormModel
    {
        [Display(Name = "Current category name")]
        public string CurrentCategoryName { get; set; }

        [Required]
        [StringLength(
            NameMaxLength,
            MinimumLength = NameMinLength,
            ErrorMessage = DefaultNameErrorMessage)]
        [Display(Name = "New category name")]
        public string NewCategoryName { get; init; }
    }
}
