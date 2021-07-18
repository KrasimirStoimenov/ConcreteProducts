namespace ConcreteProducts.Web.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using ConcreteProducts.Web.Data;
    using ConcreteProducts.Web.Data.Models;
    using ConcreteProducts.Web.Models.Categories;

    public class CategoriesController : Controller
    {
        private readonly ConcreteProductsDbContext data;

        public CategoriesController(ConcreteProductsDbContext data)
            => this.data = data;

        public IActionResult All(int id = 1)
        {
            const int itemsPerPage = 8;

            var categories = this.data.Categories
                .Select(c => new CategoryListingViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    ProductsCount = c.Products.Count
                })
                .OrderBy(c => c.Id)
                .ToList();

            var categoriesViewModel = new ListAllCategoriesViewModel
            {
                AllCategories = categories.Skip((id - 1) * itemsPerPage).Take(itemsPerPage),
                PageNumber = id,
                Count = categories.Count,
                ItemsPerPage = 12
            };

            return View(categoriesViewModel);
        }

        public IActionResult Add()
            => View(new AddCategoryFormModel());

        [HttpPost]
        public IActionResult Add(AddCategoryFormModel category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            var currentCategory = new Category
            {
                Name = category.Name
            };

            this.data.Categories.Add(currentCategory);
            this.data.SaveChanges();

            return RedirectToAction("All");
        }
    }
}
