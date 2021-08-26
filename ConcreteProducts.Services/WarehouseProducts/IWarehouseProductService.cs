namespace ConcreteProducts.Services.WarehouseProducts
{
    using System.Collections.Generic; 

    using ConcreteProducts.Services.WarehouseProducts.Models;

    public interface IWarehouseProductService
    {
        void AddProductToWarehouse(int productColorId, int warehouseId, int count);

        int AvailableQuantity(int productColorId, int warehouseId);

        void DecreaseQuantityFromProductsInWarehouse(int productColorId, int warehouseId, int count);

        IEnumerable<WarehouseProductsServiceModel> GetAllProductsInWarehouse();
    }
}
