﻿namespace ConcreteProducts.Web.Infrastructure.ValidationAttributes
{
    using System.ComponentModel.DataAnnotations;
    using ConcreteProducts.Services.Colors;

    public class IsValidColorId : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var service = validationContext.GetService(typeof(IColorService)) as IColorService;

            if (!service.IsColorExist((int)value))
            {
                return new ValidationResult("Color does not exist.");
            }

            return ValidationResult.Success;
        }
    }
}
