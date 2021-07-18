namespace ConcreteProducts.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ConcreteProducts.Web.Data;
    using ConcreteProducts.Web.Data.Models;
    using ConcreteProducts.Web.Models.Warehouses;
    using System.Linq;

    public class WarehousesController : Controller
    {
        private readonly ConcreteProductsDbContext data;

        public WarehousesController(ConcreteProductsDbContext data)
            => this.data = data;

        public IActionResult All(int id = 1)
        {
            const int itemsPerPage = 8;

            var warehouses = this.data.Warehouses
                .Select(w => new WarehouseListingViewModel
                {
                    Id = w.Id,
                    Name = w.Name,
                    TotalProductsCount = w.Products.Count,
                    TotalShapesCount = w.Shapes.Count
                })
                .OrderBy(w => w.Id)
                .ToList();

            var warehousesViewModel = new ListAllWarehouseViewModel
            {
                AllWarehouses = warehouses.Skip((id - 1) * itemsPerPage).Take(itemsPerPage),
                PageNumber = id,
                Count = warehouses.Count,
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
