namespace ConcreteProducts.Web.Controllers.Api
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using ConcreteProducts.Services.Products;
    using ConcreteProducts.Services.Products.Models;

    [Route("/api/products")]
    [ApiController]
    public class ProductsApiController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsApiController(IProductService productService)
            => this.productService = productService;

        [HttpGet]
        public async Task<IEnumerable<ProductListingServiceModel>> GetAllProducts()
            => await this.productService.GetAllListingProductsAsync(searchTerm: null);

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetailsServiceModel>> GetProductDetails(int id)
        {
            var product = await this.productService.GetProductDetailsAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
    }
}
