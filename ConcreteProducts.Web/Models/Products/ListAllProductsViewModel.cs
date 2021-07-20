namespace ConcreteProducts.Web.Models.Products
{
    using System.Collections.Generic;
    using ConcreteProducts.Web.Services.Products.Dtos;

    public class ListAllProductsViewModel : PagingViewModel
    {
        public IEnumerable<ProductServiceModel> AllProducts { get; init; }
    }
}
