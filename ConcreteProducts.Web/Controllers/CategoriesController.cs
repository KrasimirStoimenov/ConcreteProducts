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

        public IActionResult All()
        {
            var categories = this.data.Categories
                .Select(c => new CategoryListingModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    ProductsCount = c.Products.Count
                })
                .OrderBy(c=>c.Id)
                .ToList();

            return View(categories);
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

            return RedirectToAction("All", "Categories");
        }
    }
}
