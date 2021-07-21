namespace ConcreteProducts.Web.Services.Products
{
    using System.Collections.Generic;
    using ConcreteProducts.Web.Services.Products.Dtos;

    public interface IProductService
    {
        IEnumerable<ProductServiceModel> GetAllProducts(string searchTerm);
    }
}
