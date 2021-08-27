namespace ConcreteProducts.Services.Shapes
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using ConcreteProducts.Services.Shapes.Models;

    public interface IShapeService
    {
        Task<IEnumerable<ShapeBaseServiceModel>> GetAllShapesAsync();

        Task<IEnumerable<ShapeAndWarehouseServiceModel>> GetAllShapesWithWarehouseAsync();

        Task<ShapeBaseServiceModel> GetShapeToDeleteByIdAsync(int id);

        Task<int> CreateAsync(string name, string dimensions, int warehouseId);

        Task EditAsync(int id, string name, string dimensions, int warehouseId);

        Task<ShapeDetailsServiceModel> GetShapeDetailsAsync(int id);

        Task<bool> IsShapeExistAsync(int id);

        Task<bool> HasShapeWithSameNameAsync(string name);

        Task DeleteShapeAsync(int id);
    }
}
