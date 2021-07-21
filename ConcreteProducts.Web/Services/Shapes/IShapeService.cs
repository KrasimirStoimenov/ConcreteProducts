namespace ConcreteProducts.Web.Services.Shapes
{
    using System.Collections.Generic;
    using ConcreteProducts.Web.Services.Shapes.Dto;

    public interface IShapeService
    {
        IEnumerable<ShapeServiceModel> GetAllShapes();

        IEnumerable<ShapeAndWarehouseServiceModel> GetAllShapesWithWarehouse();

        bool IsShapeExist(int id);
    }
}
