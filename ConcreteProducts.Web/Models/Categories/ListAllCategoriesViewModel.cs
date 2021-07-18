namespace ConcreteProducts.Web.Models.Categories
{
    using System.Collections.Generic;

    public class ListAllCategoriesViewModel : PagingViewModel
    {
        public IEnumerable<CategoryListingViewModel> AllCategories { get; init; }
    }
}
