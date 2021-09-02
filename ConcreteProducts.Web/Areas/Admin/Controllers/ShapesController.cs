namespace ConcreteProducts.Web.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using ConcreteProducts.Services.Shapes;
    using ConcreteProducts.Services.Warehouses;
    using ConcreteProducts.Web.Areas.Admin.Models.Shapes;
    using Microsoft.AspNetCore.Mvc;

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
                ItemsPerPage = ItemsPerPage,
            };

            return this.View(shapesViewModel);
        }

        public async Task<IActionResult> Add()
            => this.View(new ShapeFormModel
            {
                Warehouses = await this.warehouseService.GetAllWarehousesAsync(),
            });

        [HttpPost]
        public async Task<IActionResult> Add(ShapeFormModel shape)
        {
            if (await this.shapeService.HasShapeWithSameNameAsync(shape.Name))
            {
                this.ModelState.AddModelError(nameof(shape.Name), this.takenShapeNameErrorMessage);
            }

            if (!this.ModelState.IsValid)
            {
                shape.Warehouses = await this.warehouseService.GetAllWarehousesAsync();

                return this.View(shape);
            }

            await this.shapeService.CreateAsync(shape.Name, shape.Dimensions, shape.WarehouseId);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!await this.shapeService.IsShapeExistAsync(id))
            {
                return this.BadRequest(this.notExistingShapeErrorMessage);
            }

            var shapeDetails = await this.shapeService.GetShapeDetailsAsync(id);
            var warehouses = await this.warehouseService.GetAllWarehousesAsync();

            return this.View(new ShapeFormModel
            {
                Name = shapeDetails.Name,
                Dimensions = shapeDetails.Dimensions,
                WarehouseId = shapeDetails.WarehouseId,
                Warehouses = warehouses,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ShapeFormModel shape)
        {
            if (!await this.shapeService.IsShapeExistAsync(id))
            {
                this.ModelState.AddModelError(nameof(shape.Name), this.notExistingShapeErrorMessage);
            }

            if (!this.ModelState.IsValid)
            {
                shape.Warehouses = await this.warehouseService.GetAllWarehousesAsync();

                return this.View(shape);
            }

            await this.shapeService.EditAsync(id, shape.Name, shape.Dimensions, shape.WarehouseId);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!await this.shapeService.IsShapeExistAsync(id))
            {
                return this.BadRequest(this.notExistingShapeErrorMessage);
            }

            var shape = await this.shapeService.GetShapeToDeleteByIdAsync(id);

            return this.View(shape);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.shapeService.DeleteShapeAsync(id);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
