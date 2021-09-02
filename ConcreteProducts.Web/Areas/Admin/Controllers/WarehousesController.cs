namespace ConcreteProducts.Web.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using ConcreteProducts.Services.Warehouses;
    using ConcreteProducts.Web.Areas.Admin.Models.Warehouses;
    using Microsoft.AspNetCore.Mvc;

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
                ItemsPerPage = ItemsPerPage,
            };

            return this.View(warehousesViewModel);
        }

        public IActionResult Add()
            => this.View(new WarehouseFormModel());

        [HttpPost]
        public async Task<IActionResult> Add(WarehouseFormModel warehouse)
        {
            if (await this.warehouseService.HasWarehouseWithSameNameAsync(warehouse.Name))
            {
                this.ModelState.AddModelError(nameof(warehouse.Name), this.takenWarehouseNameErrorMessage);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(warehouse);
            }

            await this.warehouseService.CreateAsync(warehouse.Name);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!await this.warehouseService.IsWarehouseExistAsync(id))
            {
                return this.BadRequest(this.notExistingWarehouseErrorMessage);
            }

            var warehouseDetails = await this.warehouseService.GetWarehouseDetailsAsync(id);

            return this.View(new WarehouseFormModel
            {
                Name = warehouseDetails.Name,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, WarehouseFormModel warehouse)
        {
            if (!await this.warehouseService.IsWarehouseExistAsync(id))
            {
                this.ModelState.AddModelError(nameof(warehouse.Name), this.notExistingWarehouseErrorMessage);
            }

            if (await this.warehouseService.HasWarehouseWithSameNameAsync(warehouse.Name))
            {
                this.ModelState.AddModelError(nameof(warehouse.Name), this.takenWarehouseNameErrorMessage);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(warehouse);
            }

            await this.warehouseService.EditAsync(id, warehouse.Name);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!await this.warehouseService.IsWarehouseExistAsync(id))
            {
                return this.BadRequest(this.notExistingWarehouseErrorMessage);
            }

            var warehouse = await this.warehouseService.GetWarehouseToDeleteByIdAsync(id);

            return this.View(warehouse);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.warehouseService.DeleteWarehouseAsync(id);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
