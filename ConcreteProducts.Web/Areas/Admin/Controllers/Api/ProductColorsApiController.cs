using ConcreteProducts.Services.ProductColors;
using ConcreteProducts.Services.ProductColors.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConcreteProducts.Web.Areas.Admin.Controllers.Api
{
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
