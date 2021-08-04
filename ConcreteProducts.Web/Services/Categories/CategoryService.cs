﻿namespace ConcreteProducts.Web.Services.Categories
{
    using System.Linq;
    using System.Collections.Generic;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using ConcreteProducts.Web.Data;
    using ConcreteProducts.Web.Services.Categories.Models;
    using ConcreteProducts.Web.Data.Models;

    public class CategoryService : ICategoryService
    {
        private readonly ConcreteProductsDbContext data;
        private readonly IMapper mapper;

        public CategoryService(ConcreteProductsDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public IEnumerable<CategoryServiceModel> GetAllCategories()
            => this.data.Categories
                .ProjectTo<CategoryServiceModel>(this.mapper.ConfigurationProvider)
                .ToList();

        public IEnumerable<CategoryWithProducts> GetAllCategoriesWithTheirProducts()
            => this.data.Categories
                .ProjectTo<CategoryWithProducts>(this.mapper.ConfigurationProvider)
                .OrderBy(c => c.Id)
                .ToList();

        public CategoryWithProducts GetCategoryToDelete(int id)
            => this.data.Categories
                .Where(c => c.Id == id)
                .ProjectTo<CategoryWithProducts>(this.mapper.ConfigurationProvider)
                .FirstOrDefault();

        public int Create(string name)
        {
            var category = new Category
            {
                Name = name
            };

            this.data.Categories.Add(category);
            this.data.SaveChanges();

            return category.Id;
        }

        public void Edit(int id, string name)
        {
            var category = this.data.Categories.Find(id);

            category.Name = name;

            this.data.SaveChanges();
        }

        public CategoryServiceModel GetCategoryDetails(int id)
            => this.data.Categories
                .Where(c => c.Id == id)
                .ProjectTo<CategoryServiceModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefault();

        public bool IsCategoryExist(int id)
            => this.data.Categories
                .Any(c => c.Id == id);

        public bool HasCategoryWithSameName(string name)
            => this.data.Categories
                .Any(c => c.Name == name);

        public void DeleteCategory(int id)
        {
            var category = this.data.Categories.Find(id);

            this.data.Categories.Remove(category);
            this.data.SaveChanges();
        }
    }
}
