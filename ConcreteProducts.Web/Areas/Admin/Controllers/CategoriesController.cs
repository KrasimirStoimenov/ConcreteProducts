namespace ConcreteProducts.Web.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using ConcreteProducts.Services.Categories;
    using ConcreteProducts.Web.Areas.Admin.Models.Categories;

    using static Common.GlobalConstants;

    public class CategoriesController : AdminController
    {
        private readonly string notExistingCategoryErrorMessage = "Category does not exist.";
        private readonly string takenCategoryNameErrorMessage = "Category name already taken.";

        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> All(int page = 1)
        {
            var categoriesWithProducts = await this.categoryService.GetAllCategoriesWithTheirProductsAsync();

            var categoriesViewModel = new ListAllCategoriesViewModel
            {
                AllCategories = categoriesWithProducts
                    .OrderBy(c => c.Name)
                    .Skip((page - 1) * ItemsPerPage)
                    .Take(ItemsPerPage),
                PageNumber = page,
                Count = categoriesWithProducts.Count(),
                ItemsPerPage = ItemsPerPage
            };

            return View(categoriesViewModel);
        }

        public IActionResult Add()
            => View(new CategoryFormModel());

        [HttpPost]
        public async Task<IActionResult> Add(CategoryFormModel category)
        {
            if (await this.categoryService.HasCategoryWithSameNameAsync(category.Name))
            {
                this.ModelState.AddModelError(nameof(category.Name), takenCategoryNameErrorMessage);
            }

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            await this.categoryService.CreateAsync(category.Name);

            return RedirectToAction("All");
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!await this.categoryService.IsCategoryExistAsync(id))
            {
                return BadRequest(notExistingCategoryErrorMessage);
            }

            var categoryDetails = await this.categoryService.GetCategoryDetailsAsync(id);

            return View(new CategoryFormModel
            {
                Name = categoryDetails.Name
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryFormModel category)
        {
            if (await this.categoryService.HasCategoryWithSameNameAsync(category.Name))
            {
                this.ModelState.AddModelError(nameof(category.Name), takenCategoryNameErrorMessage);
            }

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            await this.categoryService.EditAsync(category.Id, category.Name);

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!await this.categoryService.IsCategoryExistAsync(id))
            {
                return BadRequest(notExistingCategoryErrorMessage);
            }

            var category = await this.categoryService.GetCategoryToDeleteAsync(id);

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.categoryService.DeleteCategoryAsync(id);

            return RedirectToAction(nameof(All));
        }
    }
}
