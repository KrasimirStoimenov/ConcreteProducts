namespace ConcreteProducts.Web.Models.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ConcreteProducts.Web.Services.Categories.Dtos;
    using ConcreteProducts.Web.Services.Products.Dtos;

    public class ListAllProductsViewModel : PagingViewModel
    {
        //public string Category { get; init; }

        //public IEnumerable<CategoryServiceModel> Categories { get; init; }

        public string SearchTerm { get; init; }

        public IEnumerable<ProductServiceModel> AllProducts { get; init; }
    }
}
