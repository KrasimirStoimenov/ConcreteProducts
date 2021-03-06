namespace ConcreteProducts.Web.Models.Products
{
    using System.Collections.Generic;

    using ConcreteProducts.Services.Products.Models;

    public class ListAllProductsViewModel : PagingViewModel
    {
        // TODO: Sort by category

        // public string Category { get; init; }

        // public IEnumerable<CategoryServiceModel> Categories { get; init; }
        public string SearchTerm { get; init; }

        public IEnumerable<ProductListingServiceModel> AllProducts { get; init; }
    }
}
