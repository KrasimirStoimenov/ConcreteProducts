namespace ConcreteProducts.Web.Areas.Admin.Controllers.Api
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using ConcreteProducts.Services.Categories;
    using ConcreteProducts.Services.Categories.Models;

    [Route("/api/categories")]
    [ApiController]
    public class CategoriesApiController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoriesApiController(ICategoryService categoryService)
            => this.categoryService = categoryService;

        public async Task<IEnumerable<CategoryBaseServiceModel>> GetAllCategories()
            => await this.categoryService.GetAllCategoriesAsync();
    }
}
