namespace ConcreteProducts.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ConcreteProducts.Web.Data;
    using ConcreteProducts.Web.Data.Models;
    using ConcreteProducts.Web.Models.Warehouses;
    using System.Linq;
    using ConcreteProducts.Web.Services.Warehouses;

    public class WarehousesController : Controller
    {
        private readonly ConcreteProductsDbContext data;
        private readonly IWarehouseService warehouseService;

        public WarehousesController(ConcreteProductsDbContext data, IWarehouseService warehouseService)
        {
            this.data = data;
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
            => View(new AddWarehouseFormModel());

        [HttpPost]
        public IActionResult Add(AddWarehouseFormModel warehouse)
        {
            if (this.ModelState.IsValid)
            {
                return View(warehouse);
            }

            var currentWarehouse = new Warehouse
            {
                Name = warehouse.Name
            };

            this.data.Warehouses.Add(currentWarehouse);
            this.data.SaveChanges();

            return RedirectToAction("All");
        }
    }
}
