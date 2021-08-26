namespace ConcreteProducts.Services.Warehouses
{
    using System.Collections.Generic;
    using ConcreteProducts.Services.Warehouses.Models;

    public interface IWarehouseService
    {
        IEnumerable<WarehouseBaseServiceModel> GetAllWarehouses();

        IEnumerable<WarehouseWithProductsAndShapesCount> GetWarehousesWithProductsAndShapesCount();

        WarehouseWithProductsAndShapesCount GetWarehouseToDeleteById(int id);

        int Create(string name);

        void Edit(int id, string name);

        WarehouseBaseServiceModel GetWarehouseDetails(int id);

        bool IsWarehouseExist(int id);

        bool HasWarehouseWithSameName(string name);

        void DeleteWarehouse(int id);
    }
}
