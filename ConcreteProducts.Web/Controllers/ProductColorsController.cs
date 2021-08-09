namespace ConcreteProducts.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ConcreteProducts.Web.Services.Colors;
    using ConcreteProducts.Web.Services.Products;
    using ConcreteProducts.Web.Models.ProductColors;
    using ConcreteProducts.Web.Services.ProductColors;

    public class ProductColorsController : Controller
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
                Colors = this.productColorsService.GetColorsNotRelatedToProduct(productId)
            });

        [HttpPost]
        public IActionResult Add(AddColorToProductFormModel model)
        {
            if (!this.productService.IsProductExist(model.ProductId))
            {
                this.ModelState.AddModelError(nameof(model.ProductId), $"Product does not exist.");
            }

            if (!this.colorService.IsColorExist(model.ColorId))
            {
                this.ModelState.AddModelError(nameof(model.ColorId), $"Color does not exist.");
            }

            if (this.productColorsService.IsColorRelatedToProduct(model.ProductId, model.ColorId))
            {
                this.ModelState.AddModelError(nameof(model.ColorId), $"Color is already related to product.");
            }

            if (!ModelState.IsValid)
            {
                model.Colors = this.productColorsService.GetColorsNotRelatedToProduct(model.ProductId);

                return View(model);
            }

            this.productColorsService.AddColorToProduct(model.ProductId, model.ColorId);

            return RedirectToAction("Details", "Products", new { id = model.ProductId });
        }
    }
}
