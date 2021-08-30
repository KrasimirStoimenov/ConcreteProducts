namespace ConcreteProducts.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using ConcreteProducts.Web.Models.WarehouseProducts;
    using ConcreteProducts.Services.WarehouseProducts;
    using ConcreteProducts.Services.Warehouses;
    using ConcreteProducts.Services.ProductColors;

    using static Common.GlobalConstants;

    public class WarehouseProductsController : Controller
    {
        private readonly IWarehouseService warehouseService;
        private readonly IProductColorsService productColorsService;
        private readonly IWarehouseProductService warehouseProductService;

        public WarehouseProductsController(IWarehouseService warehouseService, IProductColorsService productColorsService, IWarehouseProductService warehouseProductService)
        {
            this.warehouseService = warehouseService;
            this.productColorsService = productColorsService;
            this.warehouseProductService = warehouseProductService;
        }

        [Authorize]
        public async Task<IActionResult> All(int page = 1)
        {
            var products = await this.warehouseProductService.GetAllProductsInWarehouseAsync();

            var listingProducts = new ListAllProductsInWarehouseViewModel
            {
                ProductsInWarehouse = products
                    .Skip((page - 1) * ItemsPerPage)
                    .Take(ItemsPerPage),
                PageNumber = page,
                Count = products.Count(),
                ItemsPerPage = ItemsPerPage
            };

            return View(listingProducts);
        }

        [Authorize]
        public async Task<IActionResult> Add()
            => View(new AddProductToWarehouseFormModel
            {
                ProductColors = await this.productColorsService.GetAllProductColorsAsync(),
                Warehouses = await this.warehouseService.GetAllWarehousesAsync(),
            });

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(AddProductToWarehouseFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Warehouses = await this.warehouseService.GetAllWarehousesAsync();
                model.ProductColors = await this.productColorsService.GetAllProductColorsAsync();

                return View(model);
            }

            await this.warehouseProductService.AddProductToWarehouseAsync(model.ProductColorId, model.WarehouseId, model.Count);

            return RedirectToAction(nameof(All));
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult DecreaseQuantity(string productName)
            => View(new DecreaseQuantityViewModel
            {
                ProductName = productName
            });

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> DecreaseQuantity(DecreaseQuantityViewModel model)
        {
            var availableQuantity = await this.warehouseProductService.AvailableQuantityAsync(model.ProductColorId, model.WarehouseId);

            if (model.Count > availableQuantity)
            {
                ModelState.AddModelError(nameof(model.Count), $"Unavailable quantity. Has only {availableQuantity} in stock.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await this.warehouseProductService.DecreaseQuantityFromProductsInWarehouseAsync(model.ProductColorId, model.WarehouseId, model.Count);

            return RedirectToAction(nameof(All));
        }
    }
}
