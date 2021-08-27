namespace ConcreteProducts.Services.Colors
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using ConcreteProducts.Services.Colors.Models;

    public interface IColorService
    {
        Task<IEnumerable<ColorBaseServiceModel>> GetAllColorsAsync();

        Task<ColorDeleteServiceModel> GetColorToDeleteByIdAsync(int id);

        Task<int> CreateAsync(string name);

        Task EditAsync(int id, string name);

        Task<ColorBaseServiceModel> GetColorDetailsAsync(int id);

        bool IsColorExist(int id);

        Task<bool> IsColorExistAsync(int id);

        Task<bool> HasColorWithSameNameAsync(string name);

        Task DeleteColorAsync(int id);
    }
}
