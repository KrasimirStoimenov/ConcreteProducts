namespace ConcreteProducts.Web.Services.Shapes
{
    using System.Collections.Generic;
    using ConcreteProducts.Web.Services.Shapes.Models;

    public interface IShapeService
    {
        IEnumerable<ShapeServiceModel> GetAllShapes();

        IEnumerable<ShapeAndWarehouseServiceModel> GetAllShapesWithWarehouse();

        ShapeServiceModel GetShapeToDeleteById(int id);

        int Create(string name, string dimensions, int warehouseId);

        void Edit(int id, string name, string dimensions, int warehouseId);

        ShapeDetailsServiceModel GetShapeDetails(int id);

        bool IsShapeExist(int id);

        bool HasShapeWithSameName(string name);

        void DeleteShape(int id);
    }
}
