namespace ConcreteProducts.Web.Services.Shapes
{
    using System.Collections.Generic;
    using ConcreteProducts.Web.Services.Shapes.Dtos;

    public interface IShapeService
    {
        IEnumerable<ShapeServiceModel> GetAllShapes();

        IEnumerable<ShapeAndWarehouseServiceModel> GetAllShapesWithWarehouse();

        ShapeServiceModel GetShapeToDeleteById(int id);

        bool IsShapeExist(int id);

        void DeleteShape(int id);
    }
}
