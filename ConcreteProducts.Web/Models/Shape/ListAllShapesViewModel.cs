namespace ConcreteProducts.Web.Models.Shape
{
    using System.Collections.Generic;
    using ConcreteProducts.Web.Services.Shapes.Dtos;

    public class ListAllShapesViewModel : PagingViewModel
    {
        public IEnumerable<ShapeAndWarehouseServiceModel> AllShapes { get; set; }
    }
}
