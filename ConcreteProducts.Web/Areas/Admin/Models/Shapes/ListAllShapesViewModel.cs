namespace ConcreteProducts.Web.Areas.Admin.Models.Shapes
{
    using System.Collections.Generic;
    using ConcreteProducts.Web.Models;
    using ConcreteProducts.Web.Services.Shapes.Dtos;

    public class ListAllShapesViewModel : PagingViewModel
    {
        public IEnumerable<ShapeAndWarehouseServiceModel> AllShapes { get; set; }
    }
}
