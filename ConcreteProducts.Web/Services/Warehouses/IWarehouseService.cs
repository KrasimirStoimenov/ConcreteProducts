namespace ConcreteProducts.Web.Services.Warehouses
{
    using System.Collections.Generic;
    using ConcreteProducts.Web.Services.Warehouses.Models;

    public interface IWarehouseService
    {
        IEnumerable<WarehouseServiceModel> GetAllWarehouses();

        IEnumerable<WarehouseWithProductsAndShapesCount> GetWarehousesWithProductsAndShapesCount();

        WarehouseWithProductsAndShapesCount GetWarehouseToDeleteById(int id);

        int Create(string name);

        void Edit(int id, string name);

        WarehouseServiceModel GetWarehouseDetails(int id);

        bool IsWarehouseExist(int id);

        bool HasWarehouseWithSameName(string name);

        void DeleteWarehouse(int id);
    }
}
