namespace ConcreteProducts.Web.Models.Products
{
    using System.Collections.Generic;

    public class ListAllProductsViewModel : PagingViewModel
    {
        public IEnumerable<ProductListingViewModel> AllProducts { get; init; }
    }
}
