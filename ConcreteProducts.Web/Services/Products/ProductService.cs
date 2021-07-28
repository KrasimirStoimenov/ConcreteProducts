namespace ConcreteProducts.Web.Services.Products
{
    using System.Linq;
    using System.Collections.Generic;
    using ConcreteProducts.Web.Data;
    using ConcreteProducts.Web.Services.Products.Dtos;
    using Microsoft.EntityFrameworkCore;

    public class ProductService : IProductService
    {
        private readonly ConcreteProductsDbContext data;

        public ProductService(ConcreteProductsDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<ProductServiceModel> GetAllProducts(string searchTerm)
        {
            var productQuery = this.data.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productQuery = productQuery.Where(p => p.Name.Contains(searchTerm) || p.Dimensions.Contains(searchTerm));
            }

            var products = productQuery
                .OrderByDescending(p => p.Id)
                .Select(p => new ProductServiceModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Dimensions = p.Dimensions,
                    InPallet = $"{p.QuantityInPalletInPieces} pieces / {p.QuantityInPalletInUnitOfMeasurement}{p.UnitOfMeasurement}",
                    CategoryName = p.Category.Name,
                    DefaultImageUrl = p.ProductColors
                                        .Select(pc => pc.ImageUrl)
                                        .FirstOrDefault(),
                })
                .ToList();

            return products;
        }

        public IEnumerable<ProductsInWarehouseViewModel> GetAllProductsInWarehouse()
            => this.data.Products
                .Select(p => new ProductsInWarehouseViewModel
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToList();

        public ProductDetailsServiceModel GetProductDetails(int id)
            => this.data.Products
                .Where(p => p.Id == id)
                .Select(p => new ProductDetailsServiceModel
                {
                    Name = p.Name,
                    Dimensions = p.Dimensions,
                    QuantityInPalletInUnitOfMeasurement = p.QuantityInPalletInUnitOfMeasurement,
                    QuantityInPalletInPieces = p.QuantityInPalletInPieces,
                    CountInUnitOfMeasurement = p.CountInUnitOfMeasurement,
                    UnitOfMeasurement = p.UnitOfMeasurement.ToString(),
                    Weight = p.Weight,
                    CategoryName = p.Category.Name,
                    DefaultImageUrl = p.ProductColors
                                        .Select(pc => pc.ImageUrl)
                                        .FirstOrDefault(),
                    AvailableColorsName = p.ProductColors.Select(pc => pc.Color.Name).ToList(),
                })
                .FirstOrDefault();

        public ProductDeleteServiceModel GetProductToDeleteById(int id)
            => this.data.Products
                .Where(p => p.Id == id)
                .Select(p => new ProductDeleteServiceModel
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .FirstOrDefault();

        public bool IsProductExist(int id)
            => this.data.Products.Any(p => p.Id == id);

        public void DeleteProduct(int id)
        {
            var product = this.data.Products
                .Include(p => p.ProductColors)
                .FirstOrDefault(p => p.Id == id);

            this.data.ProductColors.RemoveRange(product.ProductColors);
            this.data.Products.Remove(product);
            this.data.SaveChanges();
        }


    }
}
