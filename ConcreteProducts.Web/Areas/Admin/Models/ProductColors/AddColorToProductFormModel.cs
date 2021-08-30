namespace ConcreteProducts.Web.Areas.Admin.Models.ProductColors
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ConcreteProducts.Services.Colors.Models;
    using ConcreteProducts.Web.Infrastructure.ValidationAttributes;

    public class AddColorToProductFormModel
    {
        public int ProductId { get; init; }

        [Display(Name = "Color")]
        [IsValidColorId]
        public int ColorId { get; init; }

        public IEnumerable<ColorBaseServiceModel> Colors { get; set; }
    }
}
