namespace ConcreteProducts.Services.Products
{
    using System.Collections.Generic;

    using ConcreteProducts.Data.Models.Enumerations;
    using ConcreteProducts.Services.Products.Models;

    public interface IProductService
    {
        IEnumerable<ProductListingServiceModel> GetAllListingProducts(string searchTerm);

        List<ProductListingServiceModel> GetLatestProducts();

        ProductDetailsServiceModel GetProductDetails(int id);

        ProductBaseServiceModel GetProductToDeleteById(int id);

        int Create(string name,
            string dimensions,
            double quantityInPalletInUnitOfMeasurement,
            double quantityInPalletInPieces,
            double countInUnitOfMeasurement,
            UnitOfMeasurement unitOfMeasurement,
            double weight,
            string imageUrl,
            int categoryId,
            int colorId);

        bool IsProductExist(int id);

        bool HasProductWithSameNameAndDimensions(string name, string dimensions);

        void DeleteProduct(int id);
    }
}
