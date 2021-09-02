namespace ConcreteProducts.Web.Areas.Admin.Controllers.Api
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ConcreteProducts.Services.Warehouses;
    using ConcreteProducts.Services.Warehouses.Models;
    using Microsoft.AspNetCore.Mvc;

    [Route("/api/warehouses")]
    [ApiController]
    public class WarehousesApiController : ControllerBase
    {
        private readonly IWarehouseService warehouseService;

        public WarehousesApiController(IWarehouseService warehouseService)
            => this.warehouseService = warehouseService;

        public async Task<IEnumerable<WarehouseBaseServiceModel>> GetAllWarehouses()
            => await this.warehouseService.GetAllWarehousesAsync();
    }
}
