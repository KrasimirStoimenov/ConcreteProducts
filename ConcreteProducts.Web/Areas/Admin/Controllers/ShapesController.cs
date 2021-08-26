namespace ConcreteProducts.Web.Areas.Admin.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;

    using ConcreteProducts.Services.Shapes;
    using ConcreteProducts.Services.Warehouses;
    using ConcreteProducts.Web.Areas.Admin.Models.Shapes;

    using static GlobalConstants;

    public class ShapesController : AdminController
    {
        private readonly string notExistingShapeErrorMessage = "Shape does not exist.";
        private readonly string takenShapeNameErrorMessage = "Shape name already taken.";
        private readonly string notExistingWarehouseErrorMessage = "Warehouse does not exist.";

        private readonly IShapeService shapeService;
        private readonly IWarehouseService warehouseService;

        public ShapesController(IShapeService shapeService, IWarehouseService warehouseService)
        {
            this.shapeService = shapeService;
            this.warehouseService = warehouseService;
        }

        public IActionResult All(int page = 1)
        {
            var shapes = this.shapeService.GetAllShapesWithWarehouse();

            var shapesViewModel = new ListAllShapesViewModel
            {
                AllShapes = shapes
                    .OrderBy(s => s.Name)
                    .Skip((page - 1) * ItemsPerPage)
                    .Take(ItemsPerPage),
                PageNumber = page,
                Count = shapes.Count(),
                ItemsPerPage = ItemsPerPage
            };

            return View(shapesViewModel);
        }

        public IActionResult Add()
            => View(new ShapeFormModel
            {
                Warehouses = this.warehouseService.GetAllWarehouses()
            });

        [HttpPost]
        public IActionResult Add(ShapeFormModel shape)
        {
            if (!this.warehouseService.IsWarehouseExist(shape.WarehouseId))
            {
                this.ModelState.AddModelError(nameof(shape.WarehouseId), notExistingWarehouseErrorMessage);
            }

            if (this.shapeService.HasShapeWithSameName(shape.Name))
            {
                this.ModelState.AddModelError(nameof(shape.Name), takenShapeNameErrorMessage);
            }

            if (!this.ModelState.IsValid)
            {
                shape.Warehouses = this.warehouseService.GetAllWarehouses();

                return View(shape);
            }

            this.shapeService.Create(shape.Name, shape.Dimensions, shape.WarehouseId);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Edit(int id)
        {
            if (!this.shapeService.IsShapeExist(id))
            {
                return BadRequest(notExistingShapeErrorMessage);
            }

            var shapeDetails = this.shapeService.GetShapeDetails(id);
            var warehouses = this.warehouseService.GetAllWarehouses();

            return View(new ShapeFormModel
            {
                Name = shapeDetails.Name,
                Dimensions = shapeDetails.Dimensions,
                WarehouseId = shapeDetails.WarehouseId,
                Warehouses = warehouses
            });
        }

        [HttpPost]
        public IActionResult Edit(int id, ShapeFormModel shape)
        {
            if (!this.shapeService.IsShapeExist(id))
            {
                this.ModelState.AddModelError(nameof(shape.Name), notExistingShapeErrorMessage);
            }

            if (!ModelState.IsValid)
            {
                shape.Warehouses = this.warehouseService.GetAllWarehouses();

                return View(shape);
            }

            this.shapeService.Edit(id, shape.Name, shape.Dimensions, shape.WarehouseId);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Delete(int id)
        {
            if (!this.shapeService.IsShapeExist(id))
            {
                return BadRequest(notExistingShapeErrorMessage);
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
