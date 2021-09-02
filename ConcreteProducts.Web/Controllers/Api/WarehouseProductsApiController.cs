namespace ConcreteProducts.Web.Controllers.Api
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ConcreteProducts.Services.WarehouseProducts;
    using ConcreteProducts.Services.WarehouseProducts.Models;
    using Microsoft.AspNetCore.Mvc;

    [Route("/api/warehouseProducts")]
    [ApiController]
    public class WarehouseProductsApiController : ControllerBase
    {
        private readonly IWarehouseProductService warehouseProductService;

        public WarehouseProductsApiController(IWarehouseProductService warehouseProductService)
            => this.warehouseProductService = warehouseProductService;

        [HttpGet]
        public async Task<IEnumerable<WarehouseProductsServiceModel>> GetAllWarehouseProducts()
            => await this.warehouseProductService.GetAllProductsInWarehouseAsync();
    }
}
