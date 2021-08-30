namespace ConcreteProducts.Web.Infrastructure.ValidationAttributes
{
    using ConcreteProducts.Services.Warehouses;

    using System.ComponentModel.DataAnnotations;

    public class IsValidWarehouseId : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var service = validationContext.GetService(typeof(IWarehouseService)) as IWarehouseService;

            if (!service.IsWarehouseExistAsync((int)value).GetAwaiter().GetResult())
            {
                return new ValidationResult("Warehouse does not exist.");
            }

            return ValidationResult.Success;
        }
    }
}
