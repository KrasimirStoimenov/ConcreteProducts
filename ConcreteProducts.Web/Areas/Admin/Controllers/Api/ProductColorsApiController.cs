namespace ConcreteProducts.Web.Areas.Admin.Controllers.Api
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using ConcreteProducts.Services.ProductColors;
    using ConcreteProducts.Services.ProductColors.Model;

    [Route("/api/productColors")]
    [ApiController]
    public class ProductColorsApiController : ControllerBase
    {
        private readonly IProductColorsService productColorsService;

        public ProductColorsApiController(IProductColorsService productColorsService) 
            => this.productColorsService = productColorsService;

        public async Task<IEnumerable<ProductColorBaseServiceModel>> GetAllProductColors()
            => await this.productColorsService.GetAllProductColorsAsync();
    }
}
