namespace ConcreteProducts.Web.Services.Products
{
    using System.Collections.Generic;
    using ConcreteProducts.Web.Services.Products.Dtos;

    public interface IProductService
    {
        IEnumerable<ProductInfoServiceModel> GetAllProducts(string searchTerm);

        ProductDetailsServiceModel GetProductDetails(int id);

        ProductDeleteServiceModel GetProductToDeleteById(int id);

        void AddColorToProduct(int productId, int colorId);

        bool IsProductExist(int id);

        bool HasProductWithSameNameAndDimensions(string name, string dimensions);

        void DeleteProduct(int id);
    }
}
