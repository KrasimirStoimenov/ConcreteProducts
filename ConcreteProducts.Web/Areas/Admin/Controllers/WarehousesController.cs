namespace ConcreteProducts.Web.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using ConcreteProducts.Services.Warehouses;
    using ConcreteProducts.Web.Areas.Admin.Models.Warehouses;

    using static Common.GlobalConstants;

    public class WarehousesController : AdminController
    {
        private readonly string notExistingWarehouseErrorMessage = "Warehouse does not exist.";
        private readonly string takenWarehouseNameErrorMessage = "Warehouse name already taken.";
        private readonly IWarehouseService warehouseService;

        public WarehousesController(IWarehouseService warehouseService)
        {
            this.warehouseService = warehouseService;
        }

        public async Task<IActionResult> All(int page = 1)
        {
            var warehouses = await this.warehouseService.GetWarehousesWithProductsAndShapesCountAsync();

            var warehousesViewModel = new ListAllWarehouseViewModel
            {
                AllWarehouses = warehouses
                    .OrderBy(w => w.Name)
                    .Skip((page - 1) * ItemsPerPage)
                    .Take(ItemsPerPage),
                PageNumber = page,
                Count = warehouses.Count(),
                ItemsPerPage = ItemsPerPage
            };

            return View(warehousesViewModel);
        }

        public IActionResult Add()
            => View(new WarehouseFormModel());

        [HttpPost]
        public async Task<IActionResult> Add(WarehouseFormModel warehouse)
        {
            if (await this.warehouseService.HasWarehouseWithSameNameAsync(warehouse.Name))
            {
                this.ModelState.AddModelError(nameof(warehouse.Name), takenWarehouseNameErrorMessage);
            }

            if (!this.ModelState.IsValid)
            {
                return View(warehouse);
            }

            await this.warehouseService.CreateAsync(warehouse.Name);

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!await this.warehouseService.IsWarehouseExistAsync(id))
            {
                return BadRequest(notExistingWarehouseErrorMessage);
            }

            var warehouseDetails = await this.warehouseService.GetWarehouseDetailsAsync(id);

            return View(new WarehouseFormModel
            {
                Name = warehouseDetails.Name
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, WarehouseFormModel warehouse)
        {
            if (!await this.warehouseService.IsWarehouseExistAsync(id))
            {
                this.ModelState.AddModelError(nameof(warehouse.Name), notExistingWarehouseErrorMessage);
            }

            if (await this.warehouseService.HasWarehouseWithSameNameAsync(warehouse.Name))
            {
                this.ModelState.AddModelError(nameof(warehouse.Name), takenWarehouseNameErrorMessage);
            }

            if (!ModelState.IsValid)
            {
                return View(warehouse);
            }

            await this.warehouseService.EditAsync(id, warehouse.Name);

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!await this.warehouseService.IsWarehouseExistAsync(id))
            {
                return BadRequest(notExistingWarehouseErrorMessage);
            }

            var warehouse = await this.warehouseService.GetWarehouseToDeleteByIdAsync(id);

            return View(warehouse);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.warehouseService.DeleteWarehouseAsync(id);

            return RedirectToAction(nameof(All));
        }
    }
}
