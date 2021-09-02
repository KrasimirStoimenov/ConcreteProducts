namespace ConcreteProducts.Web.Areas.Admin.Models.Shapes
{
    using System.Collections.Generic;

    using ConcreteProducts.Services.Shapes.Models;
    using ConcreteProducts.Web.Models;

    public class ListAllShapesViewModel : PagingViewModel
    {
        public IEnumerable<ShapeAndWarehouseServiceModel> AllShapes { get; set; }
    }
}
