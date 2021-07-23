namespace ConcreteProducts.Web.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using ConcreteProducts.Web.Data;
    using ConcreteProducts.Web.Models.Shape;
    using ConcreteProducts.Web.Data.Models;
    using ConcreteProducts.Web.Services.Warehouses;
    using ConcreteProducts.Web.Services.Shapes;

    public class ShapesController : Controller
    {
        private readonly ConcreteProductsDbContext data;
        private readonly IShapeService shapeService;
        private readonly IWarehouseService warehouseService;

        public ShapesController(ConcreteProductsDbContext data, IShapeService shapeService, IWarehouseService warehouseService)
        {
            this.data = data;
            this.shapeService = shapeService;
            this.warehouseService = warehouseService;
        }

        public IActionResult All(int id = 1)
        {
            const int itemsPerPage = 8;

            var shapes = this.shapeService.GetAllShapesWithWarehouse();

            var shapesViewModel = new ListAllShapesViewModel
            {
                AllShapes = shapes.Skip((id - 1) * itemsPerPage).Take(itemsPerPage),
                PageNumber = id,
                Count = shapes.Count(),
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

        public IActionResult Edit(int id)
            => View(new EditShapeFormModel
            {
                CurrentShapeName = this.data.Shapes
                    .Where(c => c.Id == id)
                    .Select(c => c.Name)
                    .FirstOrDefault(),
                CurrentShapeDimensions = this.data.Shapes
                    .Where(c => c.Id == id)
                    .Select(c => c.Dimensions)
                    .FirstOrDefault(),
            });

        [HttpPost]
        public IActionResult Edit(int id, EditShapeFormModel shape)
        {
            if (!this.shapeService.IsShapeExist(id))
            {
                this.ModelState.AddModelError(nameof(shape.CurrentShapeName), $"Current shape name does not exist.");
            }

            if (this.data.Shapes.Any(c => c.Name == shape.NewShapeName))
            {
                this.ModelState.AddModelError(nameof(shape.NewShapeName), $"Current shape name already exist.");
            }

            if (this.data.Shapes.Any(c => c.Name == shape.NewShapeName))
            {
                this.ModelState.AddModelError(nameof(shape.NewShapeName), $"Current shape dimensions already exist.");
            }

            if (!ModelState.IsValid)
            {
                shape.CurrentShapeName = this.data.Shapes
                    .Where(c => c.Id == id)
                    .Select(c => c.Name)
                    .FirstOrDefault();

                shape.CurrentShapeDimensions = this.data.Shapes
                    .Where(c => c.Id == id)
                    .Select(c => c.Dimensions)
                    .FirstOrDefault();

                return View(shape);
            }

            var currentShape = this.data.Shapes.Find(id);

            currentShape.Name = shape.NewShapeName;
            currentShape.Dimensions = shape.NewShapeDimensions;

            this.data.Shapes.Update(currentShape);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        public IActionResult Delete(int id)
        {
            if (!this.shapeService.IsShapeExist(id))
            {
                return BadRequest("Shape does not exist!");
            }

            var shape = this.shapeService.GetShapeToDeleteById(id);

            return View(shape);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            this.shapeService.DeleteShape(id);

            return RedirectToAction(nameof(All));
        }
    }
}
