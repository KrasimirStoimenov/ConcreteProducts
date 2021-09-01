namespace ConcreteProducts.Web.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using ConcreteProducts.Services.Shapes;
    using ConcreteProducts.Services.Warehouses;
    using ConcreteProducts.Web.Areas.Admin.Models.Shapes;

    using static Common.GlobalConstants;

    public class ShapesController : AdminController
    {
        private readonly string notExistingShapeErrorMessage = "Shape does not exist.";
        private readonly string takenShapeNameErrorMessage = "Shape name already taken.";

        private readonly IShapeService shapeService;
        private readonly IWarehouseService warehouseService;

        public ShapesController(IShapeService shapeService, IWarehouseService warehouseService)
        {
            this.shapeService = shapeService;
            this.warehouseService = warehouseService;
        }

        public async Task<IActionResult> All(int page = 1)
        {
            var shapes = await this.shapeService.GetAllShapesWithWarehouseAsync();

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

        public async Task<IActionResult> Add()
            => View(new ShapeFormModel
            {
                Warehouses = await this.warehouseService.GetAllWarehousesAsync()
            });

        [HttpPost]
        public async Task<IActionResult> Add(ShapeFormModel shape)
        {
            if (await this.shapeService.HasShapeWithSameNameAsync(shape.Name))
            {
                this.ModelState.AddModelError(nameof(shape.Name), takenShapeNameErrorMessage);
            }

            if (!this.ModelState.IsValid)
            {
                shape.Warehouses = await this.warehouseService.GetAllWarehousesAsync();

                return View(shape);
            }

            await this.shapeService.CreateAsync(shape.Name, shape.Dimensions, shape.WarehouseId);

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!await this.shapeService.IsShapeExistAsync(id))
            {
                return BadRequest(notExistingShapeErrorMessage);
            }

            var shapeDetails = await this.shapeService.GetShapeDetailsAsync(id);
            var warehouses = await this.warehouseService.GetAllWarehousesAsync();

            return View(new ShapeFormModel
            {
                Name = shapeDetails.Name,
                Dimensions = shapeDetails.Dimensions,
                WarehouseId = shapeDetails.WarehouseId,
                Warehouses = warehouses
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ShapeFormModel shape)
        {
            if (!await this.shapeService.IsShapeExistAsync(id))
            {
                this.ModelState.AddModelError(nameof(shape.Name), notExistingShapeErrorMessage);
            }

            if (!ModelState.IsValid)
            {
                shape.Warehouses = await this.warehouseService.GetAllWarehousesAsync();

                return View(shape);
            }

            await this.shapeService.EditAsync(id, shape.Name, shape.Dimensions, shape.WarehouseId);

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!await this.shapeService.IsShapeExistAsync(id))
            {
                return BadRequest(notExistingShapeErrorMessage);
            }

            var shape = await this.shapeService.GetShapeToDeleteByIdAsync(id);

            return View(shape);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.shapeService.DeleteShapeAsync(id);

            return RedirectToAction(nameof(All));
        }
    }
}
