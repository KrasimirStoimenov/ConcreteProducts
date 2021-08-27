namespace ConcreteProducts.Services.Warehouses
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using ConcreteProducts.Services.Warehouses.Models;

    public interface IWarehouseService
    {
        Task<IEnumerable<WarehouseBaseServiceModel>> GetAllWarehousesAsync();

        Task<IEnumerable<WarehouseWithProductsAndShapesCount>> GetWarehousesWithProductsAndShapesCountAsync();

        Task<WarehouseWithProductsAndShapesCount> GetWarehouseToDeleteByIdAsync(int id);

        Task<int> CreateAsync(string name);

        Task EditAsync(int id, string name);

        Task<WarehouseBaseServiceModel> GetWarehouseDetailsAsync(int id);

        Task<bool> IsWarehouseExistAsync(int id);

        Task<bool> HasWarehouseWithSameNameAsync(string name);

        Task DeleteWarehouseAsync(int id);
    }
}
