namespace ConcreteProducts.Services.Categories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using ConcreteProducts.Data;
    using ConcreteProducts.Data.Models;
    using ConcreteProducts.Services.Categories.Models;
    using Microsoft.EntityFrameworkCore;

    public class CategoryService : ICategoryService
    {
        private readonly ConcreteProductsDbContext data;
        private readonly IMapper mapper;

        public CategoryService(ConcreteProductsDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CategoryBaseServiceModel>> GetAllCategoriesAsync()
            => await this.data.Categories
                .ProjectTo<CategoryBaseServiceModel>(this.mapper.ConfigurationProvider)
                .OrderBy(c => c.Name)
                .ToListAsync();

        public async Task<IEnumerable<CategoryWithProducts>> GetAllCategoriesWithTheirProductsAsync()
            => await this.data.Categories
                .ProjectTo<CategoryWithProducts>(this.mapper.ConfigurationProvider)
                .OrderBy(c => c.Id)
                .ToListAsync();

        public async Task<CategoryWithProducts> GetCategoryToDeleteAsync(int id)
            => await this.data.Categories
                .Where(c => c.Id == id)
                .ProjectTo<CategoryWithProducts>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

        public async Task<int> CreateAsync(string name)
        {
            var category = new Category
            {
                Name = name,
            };

            await this.data.Categories.AddAsync(category);
            await this.data.SaveChangesAsync();

            return category.Id;
        }

        public async Task EditAsync(int id, string name)
        {
            var category = await this.data.Categories.FindAsync(id);

            category.Name = name;

            await this.data.SaveChangesAsync();
        }

        public async Task<CategoryBaseServiceModel> GetCategoryDetailsAsync(int id)
            => await this.data.Categories
                .Where(c => c.Id == id)
                .ProjectTo<CategoryBaseServiceModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

        public async Task<bool> IsCategoryExistAsync(int id)
            => await this.data.Categories
                .AnyAsync(c => c.Id == id);

        public async Task<bool> HasCategoryWithSameNameAsync(string name)
            => await this.data.Categories
                .AnyAsync(c => c.Name == name);

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await this.data.Categories.FindAsync(id);

            this.data.Categories.Remove(category);
            await this.data.SaveChangesAsync();
        }
    }
}
