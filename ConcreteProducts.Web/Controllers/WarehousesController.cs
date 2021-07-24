﻿namespace ConcreteProducts.Web.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using ConcreteProducts.Web.Models.Warehouses;
    using ConcreteProducts.Web.Services.Warehouses;

    public class WarehousesController : Controller
    {
        private readonly string notExistingWarehouseErrorMessage = "Warehouse does not exist.";
        private readonly string takenWarehouseNameErrorMessage = "Warehouse name already taken.";
        private readonly IWarehouseService warehouseService;

        public WarehousesController(IWarehouseService warehouseService)
        {
            this.warehouseService = warehouseService;
        }

        public IActionResult All(int id = 1)
        {
            const int itemsPerPage = 8;

            var warehouses = this.warehouseService.GetWarehousesWithProductsAndShapesCount();

            var warehousesViewModel = new ListAllWarehouseViewModel
            {
                AllWarehouses = warehouses.Skip((id - 1) * itemsPerPage).Take(itemsPerPage),
                PageNumber = id,
                Count = warehouses.Count(),
                ItemsPerPage = itemsPerPage
            };

            return View(warehousesViewModel);
        }

        public IActionResult Add()
            => View(new WarehouseFormModel());

        [HttpPost]
        public IActionResult Add(WarehouseFormModel warehouse)
        {
            if (this.warehouseService.HasWarehouseWithSameName(warehouse.Name))
            {
                this.ModelState.AddModelError(nameof(warehouse.Name), takenWarehouseNameErrorMessage);
            }

            if (!this.ModelState.IsValid)
            {
                return View(warehouse);
            }

            this.warehouseService.Create(warehouse.Name);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Edit(int id)
        {
            if (!this.warehouseService.IsWarehouseExist(id))
            {
                return BadRequest(notExistingWarehouseErrorMessage);
            }

            var warehouseDetails = this.warehouseService.GetWarehouseDetails(id);

            return View(new WarehouseFormModel
            {
                Name = warehouseDetails.Name
            });
        }

        [HttpPost]
        public IActionResult Edit(int id, WarehouseFormModel warehouse)
        {
            if (!this.warehouseService.IsWarehouseExist(id))
            {
                this.ModelState.AddModelError(nameof(warehouse.Name), notExistingWarehouseErrorMessage);
            }

            if (!ModelState.IsValid)
            {
                return View(warehouse);
            }

            this.warehouseService.Edit(id, warehouse.Name);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Delete(int id)
        {
            if (!this.warehouseService.IsWarehouseExist(id))
            {
                return BadRequest(notExistingWarehouseErrorMessage);
            }

            var warehouse = this.warehouseService.GetWarehouseToDeleteById(id);

            return View(warehouse);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            this.warehouseService.DeleteWarehouse(id);

            return RedirectToAction(nameof(All));
        }
    }
}
