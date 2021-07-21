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
            if (!this.ModelState.IsValid)
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

        public IActionResult Edit(int id)
            => View(new EditWarehouseFormModel
            {
                CurrentWarehouseName = this.data.Warehouses
                    .Where(c => c.Id == id)
                    .Select(c => c.Name)
                    .FirstOrDefault()
            });

        [HttpPost]
        public IActionResult Edit(int id, EditWarehouseFormModel warehouse)
        {
            if (!this.warehouseService.IsWarehouseExist(id))
            {
                this.ModelState.AddModelError(nameof(warehouse.CurrentWarehouseName), $"Current warehouse name does not exist.");
            }

            if (this.data.Warehouses.Any(c => c.Name == warehouse.NewWarehouseName))
            {
                this.ModelState.AddModelError(nameof(warehouse.NewWarehouseName), $"Current warehouse name already exist.");
            }

            if (!ModelState.IsValid)
            {
                warehouse.CurrentWarehouseName = this.data.Warehouses
                    .Where(c => c.Id == id)
                    .Select(c => c.Name)
                    .FirstOrDefault();

                return View(warehouse);
            }

            var currentWarehose = this.data.Warehouses.Find(id);

            currentWarehose.Name = warehouse.NewWarehouseName;

            this.data.Warehouses.Update(currentWarehose);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }
    }
}
