namespace ConcreteProducts.Web.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using ConcreteProducts.Services.Categories;
    using ConcreteProducts.Web.Areas.Admin.Models.Categories;
    using Microsoft.AspNetCore.Mvc;

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
                ItemsPerPage = ItemsPerPage,
            };

            return this.View(categoriesViewModel);
        }

        public IActionResult Add()
            => this.View(new CategoryFormModel());

        [HttpPost]
        public async Task<IActionResult> Add(CategoryFormModel category)
        {
            if (await this.categoryService.HasCategoryWithSameNameAsync(category.Name))
            {
                this.ModelState.AddModelError(nameof(category.Name), this.takenCategoryNameErrorMessage);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(category);
            }

            await this.categoryService.CreateAsync(category.Name);

            return this.RedirectToAction("All");
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!await this.categoryService.IsCategoryExistAsync(id))
            {
                return this.BadRequest(this.notExistingCategoryErrorMessage);
            }

            var categoryDetails = await this.categoryService.GetCategoryDetailsAsync(id);

            return this.View(new CategoryFormModel
            {
                Name = categoryDetails.Name,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CategoryFormModel category)
        {
            if (await this.categoryService.HasCategoryWithSameNameAsync(category.Name))
            {
                this.ModelState.AddModelError(nameof(category.Name), this.takenCategoryNameErrorMessage);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(category);
            }

            await this.categoryService.EditAsync(id, category.Name);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!await this.categoryService.IsCategoryExistAsync(id))
            {
                return this.BadRequest(this.notExistingCategoryErrorMessage);
            }

            var category = await this.categoryService.GetCategoryToDeleteAsync(id);

            return this.View(category);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.categoryService.DeleteCategoryAsync(id);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
