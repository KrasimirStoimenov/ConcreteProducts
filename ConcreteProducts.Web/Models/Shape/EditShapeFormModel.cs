namespace ConcreteProducts.Web.Models.Shape
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Shape;
    using static Data.DataConstants.ErrorMessages;

    public class EditShapeFormModel
    {
        [Display(Name = "Current shape name")]
        public string CurrentShapeName { get; set; }

        [Required]
        [StringLength(
                    NameMaxLength,
                    MinimumLength = NameMinLength,
                    ErrorMessage = DefaultNameErrorMessage)]
        [Display(Name = "New shape name")]
        public string NewShapeName { get; init; }

        [Display(Name = "Current shape dimensions")]
        public string CurrentShapeDimensions { get; set; }

        [Required]
        [StringLength(
                    NameMaxLength,
                    MinimumLength = NameMinLength,
                    ErrorMessage = DefaultNameErrorMessage)]
        [Display(Name = "New shape dimensions")]
        public string NewShapeDimensions { get; init; }
    }
}
