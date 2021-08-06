namespace ConcreteProducts.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using ConcreteProducts.Web.Models.WarehouseProducts;
    using ConcreteProducts.Web.Services.Products;
    using ConcreteProducts.Web.Services.WarehouseProducts;
    using ConcreteProducts.Web.Services.Warehouses;

    public class WarehouseProductsController : Controller
    {
        private readonly IProductService productService;
        private readonly IWarehouseService warehouseService;
        private readonly IWarehouseProductService warehouseProductService;

        public WarehouseProductsController(IProductService productService, IWarehouseService warehouseService, IWarehouseProductService warehouseProductService)
        {
            this.productService = productService;
            this.warehouseService = warehouseService;
            this.warehouseProductService = warehouseProductService;
        }

        [Authorize]
        public IActionResult All()
        {
            return View();
        }

        [Authorize]
        public IActionResult Add()
            => View(new AddProductToWarehouseFormModel
            {
                Warehouses = this.warehouseService.GetAllWarehouses(),
                Products = this.productService.GetAllProducts()
            });

        [Authorize]
        [HttpPost]
        public IActionResult Add(AddProductToWarehouseFormModel model)
        {
            if (!this.productService.IsProductExist(model.ProductId))
            {
                this.ModelState.AddModelError(nameof(model.ProductId), $"Product does not exist.");
            }

            if (!this.warehouseService.IsWarehouseExist(model.WarehouseId))
            {
                this.ModelState.AddModelError(nameof(model.WarehouseId), $"Warehouse does not exist.");
            }

            if (!ModelState.IsValid)
            {
                model.Warehouses = this.warehouseService.GetAllWarehouses();
                model.Products = this.productService.GetAllProducts();

                return View(model);
            }

            this.warehouseProductService.AddProductToWarehouse(model.ProductId, model.WarehouseId, model.Count);

            return RedirectToAction(nameof(All));
        }
    }
}
