namespace ConcreteProducts.Web.Areas.Admin.Controllers.Api
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ConcreteProducts.Services.Colors;
    using ConcreteProducts.Services.Colors.Models;
    using Microsoft.AspNetCore.Mvc;

    [Route("/api/colors")]
    [ApiController]
    public class ColorsApiController : ControllerBase
    {
        private readonly IColorService colorService;

        public ColorsApiController(IColorService colorService)
            => this.colorService = colorService;

        public async Task<IEnumerable<ColorBaseServiceModel>> GetAllColors()
            => await this.colorService.GetAllColorsAsync();
    }
}
