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

        public IActionResult Edit(int id)
            => View(new EditCategoryFormModel
            {
                CurrentCategoryName = this.data.Categories
                    .Where(c => c.Id == id)
                    .Select(c => c.Name)
                    .FirstOrDefault()
            });

        [HttpPost]
        public IActionResult Edit(int id, EditCategoryFormModel category)
        {
            if (!this.categoryService.IsCategoryExist(id))
            {
                this.ModelState.AddModelError(nameof(category.CurrentCategoryName), $"Current category name does not exist.");
            }

            if (this.data.Categories.Any(c => c.Name == category.NewCategoryName))
            {
                this.ModelState.AddModelError(nameof(category.NewCategoryName), $"Current category name already exist.");
            }

            if (!ModelState.IsValid)
            {
                category.CurrentCategoryName = this.data.Categories
                    .Where(c => c.Id == id)
                    .Select(c => c.Name)
                    .FirstOrDefault();

                return View(category);
            }

            var currentCategory = this.data.Categories.Find(id);

            currentCategory.Name = category.NewCategoryName;

            this.data.Categories.Update(currentCategory);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        public IActionResult Delete(int id)
        {
            if (!this.categoryService.IsCategoryExist(id))
            {
                return BadRequest("Category does not exist!");
            }

            var category = this.categoryService.GetCategoryToDelete(id);

            return View(category);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            this.categoryService.DeleteCategory(id);

            return RedirectToAction(nameof(All));
        }
    }
}
