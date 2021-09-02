namespace ConcreteProducts.Web.Areas.Admin.Controllers.Api
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ConcreteProducts.Services.Shapes;
    using ConcreteProducts.Services.Shapes.Models;
    using Microsoft.AspNetCore.Mvc;

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
