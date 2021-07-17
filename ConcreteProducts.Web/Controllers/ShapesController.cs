namespace ConcreteProducts.Web.Controllers
{
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using ConcreteProducts.Web.Data;
    using ConcreteProducts.Web.Models.Shape;
    using ConcreteProducts.Web.Data.Models;

    public class ShapesController : Controller
    {
        private readonly ConcreteProductsDbContext data;

        public ShapesController(ConcreteProductsDbContext data)
            => this.data = data;

        public IActionResult All()
        {
            var shapes = this.data.Shapes
                .Select(s => new ShapeListingViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Dimensions = s.Dimensions,
                    WarehouseName = s.Warehouse.Name
                })
                .OrderBy(s => s.Id)
                .ToList();

            return View(shapes);
        }

        public IActionResult Add()
            => View(new AddShapeFormModel
            {
                Warehouses = this.GetShapeWarehouses()
            });

        [HttpPost]
        public IActionResult Add(AddShapeFormModel shape)
        {
            if (!this.data.Warehouses.Any(w => w.Id == shape.WarehouseId))
            {
                this.ModelState.AddModelError(nameof(shape.WarehouseId), $"{nameof(shape.WarehouseId)} does not exist.");
            }

            if (!this.ModelState.IsValid)
            {
                shape.Warehouses = this.GetShapeWarehouses();

                return View(shape);
            }

            var currentShape = new Shape
            {
                Name = shape.Name,
                Dimensions = shape.Dimensions,
                WarehouseId = shape.WarehouseId
            };

            this.data.Shapes.Add(currentShape);
            this.data.SaveChanges();

            return RedirectToAction("All");
        }

        private IEnumerable<ShapeWarehouseViewModel> GetShapeWarehouses()
            => this.data
                .Warehouses
                .Select(w => new ShapeWarehouseViewModel
                {
                    Id = w.Id,
                    Name = w.Name
                })
                .ToList();
    }
}
