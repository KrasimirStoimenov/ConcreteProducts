namespace ConcreteProducts.Web.Services.WarehouseProducts
{
using ConcreteProducts.Web.Services.WarehouseProducts.Models;
    using System.Collections.Generic;

    public interface IWarehouseProductService
    {
        void AddProductToWarehouse(int productId, int warehouseId, int count);

        IEnumerable<WarehouseProductsServiceModel> GetAllProductsInWarehouse();
    }
}
