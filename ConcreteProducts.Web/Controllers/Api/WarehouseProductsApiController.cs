namespace ConcreteProducts.Web.Controllers.Api
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using ConcreteProducts.Services.WarehouseProducts;
    using ConcreteProducts.Services.WarehouseProducts.Models;

    [Route("/api/warehouseProducts")]
    [ApiController]
    public class WarehouseProductsApiController
    {
        private readonly IWarehouseProductService warehouseProductService;

        public WarehouseProductsApiController(IWarehouseProductService warehouseProductService)
            => this.warehouseProductService = warehouseProductService;

        [HttpGet]
        public async Task<IEnumerable<WarehouseProductsServiceModel>> GetAllWarehouseProducts()
            => await this.warehouseProductService.GetAllProductsInWarehouseAsync();
    }
}
