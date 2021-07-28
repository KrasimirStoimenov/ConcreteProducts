namespace ConcreteProducts.Web.Areas.Admin.Models.Categories
{
    using System.Collections.Generic;
    using ConcreteProducts.Web.Models;
    using ConcreteProducts.Web.Services.Categories.Dtos;

    public class ListAllCategoriesViewModel : PagingViewModel
    {
        public IEnumerable<CategoryWithProducts> AllCategories { get; init; }
    }
}
