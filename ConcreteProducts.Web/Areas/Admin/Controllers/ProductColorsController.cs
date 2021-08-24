﻿namespace ConcreteProducts.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using ConcreteProducts.Services.Colors;
    using ConcreteProducts.Services.Products;
    using ConcreteProducts.Services.ProductColors;
    using ConcreteProducts.Web.Areas.Admin.Models.ProductColors;

    public class ProductColorsController : AdminController
    {
        private readonly IProductColorsService productColorsService;
        private readonly IProductService productService;
        private readonly IColorService colorService;

        public ProductColorsController(IProductService productService, IColorService colorService, IProductColorsService productColorsService)
        {
            this.productService = productService;
            this.colorService = colorService;
            this.productColorsService = productColorsService;
        }

        public IActionResult Add(int productId)
            => View(new AddColorToProductFormModel
            {
                ProductId = productId,
                Colors = productColorsService.GetColorsNotRelatedToProduct(productId)
            });

        [HttpPost]
        public IActionResult Add(AddColorToProductFormModel model)
        {
            if (!productService.IsProductExist(model.ProductId))
            {
                ModelState.AddModelError(nameof(model.ProductId), $"Product does not exist.");
            }

            if (!colorService.IsColorExist(model.ColorId))
            {
                ModelState.AddModelError(nameof(model.ColorId), $"Color does not exist.");
            }

            if (productColorsService.IsColorRelatedToProduct(model.ProductId, model.ColorId))
            {
                ModelState.AddModelError(nameof(model.ColorId), $"Color is already related to product.");
            }

            if (!ModelState.IsValid)
            {
                model.Colors = productColorsService.GetColorsNotRelatedToProduct(model.ProductId);

                return View(model);
            }

            productColorsService.AddColorToProduct(model.ProductId, model.ColorId);

            return Redirect($"/Products/Details/{model.ProductId}");
        }
    }
}
