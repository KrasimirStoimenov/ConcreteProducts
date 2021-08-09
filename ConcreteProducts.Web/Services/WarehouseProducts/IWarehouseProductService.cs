namespace ConcreteProducts.Web.Services.WarehouseProducts
{
    using ConcreteProducts.Web.Services.WarehouseProducts.Models;
    using System.Collections.Generic;

    public interface IWarehouseProductService
    {
        void AddProductToWarehouse(int productColorId, int warehouseId, int count);

        int AvailableQuantity(int productColorId, int warehouseId);

        void DecreaseQuantityFromProductsInWarehouse(int productColorId, int warehouseId, int count);

        IEnumerable<WarehouseProductsServiceModel> GetAllProductsInWarehouse();
    }
}
