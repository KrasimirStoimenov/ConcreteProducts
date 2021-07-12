namespace ConcreteProducts.Web.Controllers
{
    using ConcreteProducts.Web.Data;
    using ConcreteProducts.Web.Data.Models;
    using ConcreteProducts.Web.Models.Categories;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : Controller
    {
        private readonly ConcreteProductsDbContext data;

        public CategoriesController(ConcreteProductsDbContext data)
            => this.data = data;

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

            return RedirectToAction("Add","Products");
        }
    }
}
