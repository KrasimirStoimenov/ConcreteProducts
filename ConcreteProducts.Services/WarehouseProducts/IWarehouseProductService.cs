namespace ConcreteProducts.Services.WarehouseProducts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ConcreteProducts.Services.WarehouseProducts.Models;

    public interface IWarehouseProductService
    {
        Task AddProductToWarehouseAsync(int productColorId, int warehouseId, int count);

        Task<int> AvailableQuantityAsync(int productColorId, int warehouseId);

        Task DecreaseQuantityFromProductsInWarehouseAsync(int productColorId, int warehouseId, int count);

        Task<IEnumerable<WarehouseProductsServiceModel>> GetAllProductsInWarehouseAsync();
    }
}
