namespace ConcreteProducts.Services.Products
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ConcreteProducts.Data.Models.Enumerations;
    using ConcreteProducts.Services.Products.Models;

    public interface IProductService
    {
        Task<IEnumerable<ProductListingServiceModel>> GetAllListingProductsAsync(string searchTerm);

        Task<List<ProductListingServiceModel>> GetLatestProductsAsync();

        Task<ProductDetailsServiceModel> GetProductDetailsAsync(int id);

        Task<ProductBaseServiceModel> GetProductToDeleteByIdAsync(int id);

        Task<int> CreateAsync(
            string name,
            string dimensions,
            double quantityInPalletInUnitOfMeasurement,
            double quantityInPalletInPieces,
            double countInUnitOfMeasurement,
            UnitOfMeasurement unitOfMeasurement,
            double weight,
            string imageUrl,
            int categoryId,
            int colorId);

        Task<bool> IsProductExistAsync(int id);

        Task<bool> HasProductWithSameNameAndDimensionsAsync(string name, string dimensions);

        Task DeleteProductAsync(int id);
    }
}
