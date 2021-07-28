namespace ConcreteProducts.Web.Areas.Admin.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using ConcreteProducts.Web.Services.Categories;
    using ConcreteProducts.Web.Areas.Admin.Models.Categories;

    using static AdminConstants;

    [Authorize(Roles = AdministratorRoleName)]
    [Area(AreaName)]
    public class CategoriesController : Controller
    {
        private readonly string notExistingCategoryErrorMessage = "Category does not exist.";
        private readonly string takenCategoryNameErrorMessage = "Category name already taken.";
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
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
                ItemsPerPage = itemsPerPage
            };

            return View(categoriesViewModel);
        }

        public IActionResult Add()
            => View(new CategoryFormModel());

        [HttpPost]
        public IActionResult Add(CategoryFormModel category)
        {
            if (this.categoryService.HasCategoryWithSameName(category.Name))
            {
                this.ModelState.AddModelError(nameof(category.Name), takenCategoryNameErrorMessage);
            }

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            this.categoryService.Create(category.Name);

            return RedirectToAction("All");
        }

        public IActionResult Edit(int id)
        {
            if (!this.categoryService.IsCategoryExist(id))
            {
                return BadRequest(notExistingCategoryErrorMessage);
            }

            var categoryDetails = this.categoryService.GetCategoryDetails(id);

            return View(new CategoryFormModel
            {
                Name = categoryDetails.Name
            });
        }

        [HttpPost]
        public IActionResult Edit(int id, CategoryFormModel category)
        {
            if (!this.categoryService.IsCategoryExist(id))
            {
                this.ModelState.AddModelError(nameof(category.Name), notExistingCategoryErrorMessage);
            }

            if (this.categoryService.HasCategoryWithSameName(category.Name))
            {
                this.ModelState.AddModelError(nameof(category.Name), takenCategoryNameErrorMessage);
            }

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            this.categoryService.Edit(id, category.Name);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Delete(int id)
        {
            if (!this.categoryService.IsCategoryExist(id))
            {
                return BadRequest(notExistingCategoryErrorMessage);
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
