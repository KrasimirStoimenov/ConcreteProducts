namespace ConcreteProducts.Web.Services.Categories
{
    using System.Collections.Generic;
    using ConcreteProducts.Web.Services.Categories.Dtos;

    public interface ICategoryService
    {
        IEnumerable<CategoriesWithProducts> GetAllCategoriesWithTheirProducts();
        IEnumerable<CategoryServiceModel> GetAllCategories();
    }
}
