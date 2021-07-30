namespace ConcreteProducts.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ConcreteProducts.Web.Models.ProductColors;
    using ConcreteProducts.Web.Services.Colors;
    using ConcreteProducts.Web.Services.Products;

    public class ProductColorsController : Controller
    {
        private readonly IProductService productService;
        private readonly IColorService colorService;

        public ProductColorsController(IProductService productService, IColorService colorService)
        {
            this.productService = productService;
            this.colorService = colorService;
        }

        public IActionResult AddColorToProduct(int productId)
            => View(new AddColorToProductViewModel
            {
                ProductId = productId,
                Colors = this.colorService.GetAllColors()
            });

        [HttpPost]
        public IActionResult AddColorToProduct(AddColorToProductViewModel model)
        {
            if (!this.colorService.IsColorExist(model.ProductId))
            {
                this.ModelState.AddModelError(nameof(model.ProductId), $"Product does not exist.");
            }

            if (!this.colorService.IsColorExist(model.ColorId))
            {
                this.ModelState.AddModelError(nameof(model.ColorId), $"Color does not exist.");
            }

            if (!ModelState.IsValid)
            {
                model.Colors = this.colorService.GetAllColors();

                return View(model);
            }

            this.productService.AddColorToProduct(model.ProductId, model.ColorId);

            return RedirectToAction("Details", "Products", new { id = model.ProductId });
        }
    }
}
