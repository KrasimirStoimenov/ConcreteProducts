namespace ConcreteProducts.Web.Services.Warehouses
{
    using System.Collections.Generic;
    using ConcreteProducts.Web.Services.Warehouses.Dtos;

    public interface IWarehouseService
    {
        IEnumerable<WarehouseServiceModel> GetAllWarehouses();

        IEnumerable<WarehouseWithProductsAndShapesCount> GetWarehousesWithProductsAndShapesCount();

        bool IsWarehouseExist(int id);

        void DeleteWarehouse(int id);
    }
}
