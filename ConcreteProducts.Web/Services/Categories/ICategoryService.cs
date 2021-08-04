namespace ConcreteProducts.Web.Services.Categories
{
    using System.Collections.Generic;
    using ConcreteProducts.Web.Services.Categories.Models;

    public interface ICategoryService
    {
        IEnumerable<CategoryWithProducts> GetAllCategoriesWithTheirProducts();

        IEnumerable<CategoryServiceModel> GetAllCategories();

        CategoryWithProducts GetCategoryToDelete(int id);

        int Create(string name);

        void Edit(int id, string name);

        CategoryServiceModel GetCategoryDetails(int id);

        bool IsCategoryExist(int id);

        bool HasCategoryWithSameName(string name);

        void DeleteCategory(int id);
    }
}
