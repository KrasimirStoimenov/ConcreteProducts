namespace ConcreteProducts.Web.Areas.Admin.Models.Categories
{
    using System.Collections.Generic;

    using ConcreteProducts.Services.Categories.Models;
    using ConcreteProducts.Web.Models;

    public class ListAllCategoriesViewModel : PagingViewModel
    {
        public IEnumerable<CategoryWithProducts> AllCategories { get; init; }
    }
}
