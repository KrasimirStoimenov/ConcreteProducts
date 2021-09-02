namespace ConcreteProducts.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;

    using ConcreteProducts.Services.Colors;
    using ConcreteProducts.Services.ProductColors;
    using ConcreteProducts.Services.Products;
    using ConcreteProducts.Web.Areas.Admin.Models.ProductColors;
    using Microsoft.AspNetCore.Mvc;

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
            => this.View(new AddColorToProductFormModel
            {
                ProductId = productId,
                Colors = await this.productColorsService.GetColorsNotRelatedToProductAsync(productId),
            });

        [HttpPost]
        public async Task<IActionResult> Add(AddColorToProductFormModel model)
        {
            if (!await this.productService.IsProductExistAsync(model.ProductId))
            {
                this.ModelState.AddModelError(nameof(model.ProductId), $"Product does not exist.");
            }

            if (await this.productColorsService.IsColorRelatedToProductAsync(model.ProductId, model.ColorId))
            {
                this.ModelState.AddModelError(nameof(model.ColorId), $"Color is already related to product.");
            }

            if (!this.ModelState.IsValid)
            {
                model.Colors = await this.productColorsService.GetColorsNotRelatedToProductAsync(model.ProductId);

                return this.View(model);
            }

            await this.productColorsService.AddColorToProductAsync(model.ProductId, model.ColorId);

            return this.Redirect($"/Products/Details/{model.ProductId}");
        }
    }
}
