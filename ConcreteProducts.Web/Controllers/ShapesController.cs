namespace ConcreteProducts.Web.Controllers
{
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using ConcreteProducts.Web.Data;
    using ConcreteProducts.Web.Models.Shape;
    using ConcreteProducts.Web.Data.Models;
    using ConcreteProducts.Web.Services.Warehouses;

    public class ShapesController : Controller
    {
        private readonly ConcreteProductsDbContext data;
        private readonly IWarehouseService warehouseService;

        public ShapesController(ConcreteProductsDbContext data, IWarehouseService warehouseService)
        {
            this.data = data;
            this.warehouseService = warehouseService;
        }

        public IActionResult All(int id = 1)
        {
            const int itemsPerPage = 8;

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

            var shapesViewModel = new ListAllShapesViewModel
            {
                AllShapes = shapes.Skip((id - 1) * itemsPerPage).Take(itemsPerPage),
                PageNumber = id,
                Count = shapes.Count,
                ItemsPerPage = itemsPerPage
            };

            return View(shapesViewModel);
        }

        public IActionResult Add()
            => View(new AddShapeFormModel
            {
                Warehouses = this.warehouseService.GetAllWarehouses()
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
                shape.Warehouses = this.warehouseService.GetAllWarehouses();

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

            return RedirectToAction(nameof(All));
        }
    }
}
