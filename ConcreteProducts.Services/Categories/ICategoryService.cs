namespace ConcreteProducts.Services.Categories
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using ConcreteProducts.Services.Categories.Models;

    public interface ICategoryService
    {
        Task<IEnumerable<CategoryWithProducts>> GetAllCategoriesWithTheirProductsAsync();

        Task<IEnumerable<CategoryBaseServiceModel>> GetAllCategoriesAsync();

        Task<CategoryWithProducts> GetCategoryToDeleteAsync(int id);

        Task<int> CreateAsync(string name);

        Task EditAsync(int id, string name);

        Task<CategoryBaseServiceModel> GetCategoryDetailsAsync(int id);

        Task<bool> IsCategoryExistAsync(int id);

        Task<bool> HasCategoryWithSameNameAsync(string name);

        Task DeleteCategoryAsync(int id);
    }
}
