namespace ConcreteProducts.Web.Services.Categories
{
    using System.Collections.Generic;
    using ConcreteProducts.Web.Services.Categories.Dtos;

    public interface ICategoryService
    {
        IEnumerable<CategoryWithProducts> GetAllCategoriesWithTheirProducts();

        IEnumerable<CategoryServiceModel> GetAllCategories();

        CategoryWithProducts GetCategoryToDelete(int id);

        bool IsCategoryExist(int id);

        void DeleteCategory(int id);
    }
}
