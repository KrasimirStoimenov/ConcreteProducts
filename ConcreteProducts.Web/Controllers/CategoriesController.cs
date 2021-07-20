namespace ConcreteProducts.Web.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using ConcreteProducts.Web.Data;
    using ConcreteProducts.Web.Data.Models;
    using ConcreteProducts.Web.Models.Categories;
    using ConcreteProducts.Web.Services.Categories;

    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly ConcreteProductsDbContext data;

        public CategoriesController(ICategoryService categoryService, ConcreteProductsDbContext data)
        {
            this.categoryService = categoryService;
            this.data = data;
        }

        public IActionResult All(int id = 1)
        {
            const int itemsPerPage = 8;

            var categoriesWithProducts = this.categoryService.GetAllCategoriesWithTheirProducts();

            var categoriesViewModel = new ListAllCategoriesViewModel
            {
                AllCategories = categoriesWithProducts
                    .Skip((id - 1) * itemsPerPage)
                    .Take(itemsPerPage),
                PageNumber = id,
                Count = categoriesWithProducts.Count(),
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
