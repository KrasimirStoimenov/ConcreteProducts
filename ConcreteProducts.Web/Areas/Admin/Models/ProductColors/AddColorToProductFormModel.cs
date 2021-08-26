namespace ConcreteProducts.Web.Areas.Admin.Models.ProductColors
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ConcreteProducts.Services.Colors.Models;

    public class AddColorToProductFormModel
    {
        public int ProductId { get; init; }

        [Display(Name = "Color")]
        public int ColorId { get; init; }

        public IEnumerable<ColorBaseServiceModel> Colors { get; set; }
    }
}
