﻿namespace ConcreteProducts.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using ConcreteProducts.Web.Models.WarehouseProducts;
    using ConcreteProducts.Web.Services.WarehouseProducts;
    using ConcreteProducts.Web.Services.Warehouses;
    using ConcreteProducts.Web.Services.ProductColors;

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
        public IActionResult All()
        {
            return View();
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
            if (!this.productColorsService.IsProductColorExist(model.ProductColorId))
            {
                this.ModelState.AddModelError(nameof(model.ProductColorId), $"Product with this color does not exist.");
            }

            if (!this.warehouseService.IsWarehouseExist(model.WarehouseId))
            {
                this.ModelState.AddModelError(nameof(model.WarehouseId), $"Warehouse does not exist.");
            }

            if (!ModelState.IsValid)
            {
                model.Warehouses = this.warehouseService.GetAllWarehouses();
                model.ProductColors = this.productColorsService.GetAllProductColors();

                return View(model);
            }

            this.warehouseProductService.AddProductToWarehouse(model.ProductColorId, model.WarehouseId, model.Count);

            return RedirectToAction(nameof(All));
        }
    }
}
