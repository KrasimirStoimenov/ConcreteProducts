namespace ConcreteProducts.Services.Shapes
{
    using System.Collections.Generic;
    using ConcreteProducts.Services.Shapes.Models;

    public interface IShapeService
    {
        IEnumerable<ShapeBaseServiceModel> GetAllShapes();

        IEnumerable<ShapeAndWarehouseServiceModel> GetAllShapesWithWarehouse();

        ShapeBaseServiceModel GetShapeToDeleteById(int id);

        int Create(string name, string dimensions, int warehouseId);

        void Edit(int id, string name, string dimensions, int warehouseId);

        ShapeDetailsServiceModel GetShapeDetails(int id);

        bool IsShapeExist(int id);

        bool HasShapeWithSameName(string name);

        void DeleteShape(int id);
    }
}
