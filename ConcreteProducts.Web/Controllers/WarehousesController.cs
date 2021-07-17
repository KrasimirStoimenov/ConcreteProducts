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

        public IActionResult All()
        {
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

            return View(warehouses);
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
