namespace ConcreteProducts.Web.Services.Categories
{
    using System.Linq;
    using System.Collections.Generic;
    using ConcreteProducts.Web.Data;
    using ConcreteProducts.Web.Services.Categories.Dtos;

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

        public IEnumerable<CategoryWithProducts> GetAllCategoriesWithTheirProducts()
            => this.data.Categories
                .Select(c => new CategoryWithProducts
                {
                    Id = c.Id,
                    Name = c.Name,
                    ProductsCount = c.Products.Count
                })
                .OrderBy(c => c.Id)
                .ToList();

        public CategoryWithProducts GetCategoryToDelete(int id)
            => this.data.Categories
                .Where(c => c.Id == id)
                .Select(c => new CategoryWithProducts
                {
                    Id = c.Id,
                    Name = c.Name,
                    ProductsCount = c.Products.Count
                })
                .FirstOrDefault();

        public bool IsCategoryExist(int id)
            => this.data.Categories
                .Any(c => c.Id == id);

        public void DeleteCategory(int id)
        {
            var category = this.data.Categories.Find(id);

            this.data.Categories.Remove(category);
            this.data.SaveChanges();
        }


    }
}
