namespace ConcreteProducts.Web.Areas.Admin.Controllers.Api
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using ConcreteProducts.Services.Shapes;
    using ConcreteProducts.Services.Shapes.Models;

    [Route("/api/shapes")]
    [ApiController]
    public class ShapesApiController : ControllerBase
    {
        private readonly IShapeService shapeService;

        public ShapesApiController(IShapeService shapeService)
            => this.shapeService = shapeService;

        public async Task<IEnumerable<ShapeBaseServiceModel>> GetAllShapes()
            => await this.shapeService.GetAllShapesAsync();
    }
}
