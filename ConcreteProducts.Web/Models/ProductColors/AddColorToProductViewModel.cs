namespace ConcreteProducts.Web.Models.ProductColors
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ConcreteProducts.Web.Services.Colors.Models;

    public class AddColorToProductViewModel
    {
        public int ProductId { get; init; }

        [Display(Name = "Color")]
        public int ColorId { get; init; }

        public IEnumerable<ColorServiceModel> Colors { get; set; }
    }
}
