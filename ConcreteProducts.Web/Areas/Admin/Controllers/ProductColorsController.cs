namespace ConcreteProducts.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;

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

        public async Task<IActionResult> Add(int productId)
            => View(new AddColorToProductFormModel
            {
                ProductId = productId,
                Colors = await productColorsService.GetColorsNotRelatedToProductAsync(productId)
            });

        [HttpPost]
        public async Task<IActionResult> Add(AddColorToProductFormModel model)
        {
            if (!await productService.IsProductExistAsync(model.ProductId))
            {
                ModelState.AddModelError(nameof(model.ProductId), $"Product does not exist.");
            }

            if (!colorService.IsColorExist(model.ColorId))
            {
                ModelState.AddModelError(nameof(model.ColorId), $"Color does not exist.");
            }

            if (await productColorsService.IsColorRelatedToProductAsync(model.ProductId, model.ColorId))
            {
                ModelState.AddModelError(nameof(model.ColorId), $"Color is already related to product.");
            }

            if (!ModelState.IsValid)
            {
                model.Colors = await productColorsService.GetColorsNotRelatedToProductAsync(model.ProductId);

                return View(model);
            }

            await productColorsService.AddColorToProductAsync(model.ProductId, model.ColorId);

            return Redirect($"/Products/Details/{model.ProductId}");
        }
    }
}
