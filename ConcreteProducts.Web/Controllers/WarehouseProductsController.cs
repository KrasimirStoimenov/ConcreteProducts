namespace ConcreteProducts.Web.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using ConcreteProducts.Web.Models.WarehouseProducts;
    using ConcreteProducts.Web.Services.WarehouseProducts;
    using ConcreteProducts.Web.Services.Warehouses;
    using ConcreteProducts.Web.Services.ProductColors;

    using static GlobalConstants;

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
        public IActionResult All(int page = 1)
        {
            var products = this.warehouseProductService.GetAllProductsInWarehouse();

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
        public IActionResult Add()
            => View(new AddProductToWarehouseFormModel
            {
                ProductColors = this.productColorsService.GetAllProductColors(),
                Warehouses = this.warehouseService.GetAllWarehouses(),
            });

        [Authorize]
        [HttpPost]
        public IActionResult Add(AddProductToWarehouseFormModel model)
        {
            this.Validator(model.ProductColorId, model.WarehouseId);

            if (!ModelState.IsValid)
            {
                model.Warehouses = this.warehouseService.GetAllWarehouses();
                model.ProductColors = this.productColorsService.GetAllProductColors();

                return View(model);
            }

            this.warehouseProductService.AddProductToWarehouse(model.ProductColorId, model.WarehouseId, model.Count);

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
        public IActionResult DecreaseQuantity(DecreaseQuantityViewModel model)
        {
            this.Validator(model.ProductColorId, model.WarehouseId);

            var availableQuantity = this.warehouseProductService.AvailableQuantity(model.ProductColorId, model.WarehouseId);

            if (model.Count > availableQuantity)
            {
                ModelState.AddModelError(nameof(model.Count), $"Unavailable quantity. Has only {availableQuantity} in stock.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.warehouseProductService.DecreaseQuantityFromProductsInWarehouse(model.ProductColorId, model.WarehouseId, model.Count);

            return RedirectToAction(nameof(All));
        }

        private void Validator(int productColorId, int warehouseId)
        {
            if (!this.productColorsService.IsProductColorExist(productColorId))
            {
                this.ModelState.AddModelError(nameof(productColorId), $"Product with this color does not exist.");
            }

            if (!this.warehouseService.IsWarehouseExist(warehouseId))
            {
                this.ModelState.AddModelError(nameof(warehouseId), $"Warehouse does not exist.");
            }
        }
    }
}
