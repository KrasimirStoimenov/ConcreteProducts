﻿namespace ConcreteProducts.Services.Categories
{
    using System.Collections.Generic;

    using ConcreteProducts.Services.Categories.Models;

    public interface ICategoryService
    {
        IEnumerable<CategoryWithProducts> GetAllCategoriesWithTheirProducts();

        IEnumerable<CategoryBaseServiceModel> GetAllCategories();

        CategoryWithProducts GetCategoryToDelete(int id);

        int Create(string name);

        void Edit(int id, string name);

        CategoryBaseServiceModel GetCategoryDetails(int id);

        bool IsCategoryExist(int id);

        bool HasCategoryWithSameName(string name);

        void DeleteCategory(int id);
    }
}