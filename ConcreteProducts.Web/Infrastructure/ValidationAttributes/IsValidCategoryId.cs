namespace ConcreteProducts.Web.Infrastructure.ValidationAttributes
{
    using System.ComponentModel.DataAnnotations;

    using ConcreteProducts.Services.Categories;

    public class IsValidCategoryId : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var service = validationContext.GetService(typeof(ICategoryService)) as ICategoryService;

            if (!service.IsCategoryExistAsync((int)value).GetAwaiter().GetResult())
            {
                return new ValidationResult("Category does not exist.");
            }

            return ValidationResult.Success;
        }
    }
}
