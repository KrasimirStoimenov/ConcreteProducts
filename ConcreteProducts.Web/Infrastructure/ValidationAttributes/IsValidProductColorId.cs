namespace ConcreteProducts.Web.Infrastructure.ValidationAttributes
{
    using System.ComponentModel.DataAnnotations;

    using ConcreteProducts.Services.ProductColors;

    public class IsValidProductColorId : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var service = validationContext.GetService(typeof(IProductColorsService)) as IProductColorsService;

            if (!service.IsProductColorExistAsync((int)value).GetAwaiter().GetResult())
            {
                return new ValidationResult("Product with this color does not exist.");
            }

            return ValidationResult.Success;
        }
    }
}
