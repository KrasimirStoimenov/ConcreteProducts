namespace ConcreteProducts.Web.Services.Categories
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using ConcreteProducts.Web.Data;
    using ConcreteProducts.Web.Services.Categories.Dtos;
    using ConcreteProducts.Web.Models.Products;
    using Microsoft.EntityFrameworkCore;

    public class CategoryService : ICategoryService
    {
        private readonly ConcreteProductsDbContext data;

        public CategoryService(ConcreteProductsDbContext data)
            => this.data = data;

        public IEnumerable<CategoryServiceModel> GetAllCategories()
            => this.data
                .Categories
                .Select(c => new CategoryServiceModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

        public IEnumerable<CategoriesWithProducts> GetAllCategoriesWithTheirProducts()
            => this.data.Categories
                .Select(c => new CategoriesWithProducts
                {
                    Id = c.Id,
                    Name = c.Name,
                    ProductsCount = c.Products.Count
                })
                .OrderBy(c => c.Id)
                .ToList();

        public bool IsCategoryExist(int id)
            => this.data.Categories.Any(c => c.Id == id);

        public void DeleteCategory(int id)
        {
            var category = this.data.Categories.Include(cp => cp.Products).FirstOrDefault(c => c.Id == id);

            if (category.Products.Any())
            {
                return;
            }
            this.data.Categories.Remove(category);
            this.data.SaveChanges();
        }
    }
}
