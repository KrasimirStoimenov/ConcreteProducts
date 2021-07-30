namespace ConcreteProducts.Web.Services.Products
{
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using ConcreteProducts.Web.Data;
    using ConcreteProducts.Web.Services.Products.Dtos;
    using ConcreteProducts.Web.Data.Models;

    public class ProductService : IProductService
    {
        private readonly ConcreteProductsDbContext data;

        public ProductService(ConcreteProductsDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<ProductInfoServiceModel> GetAllProducts(string searchTerm)
        {
            var productQuery = this.data.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productQuery = productQuery.Where(p => p.Name.Contains(searchTerm) || p.Dimensions.Contains(searchTerm));
            }

            var products = productQuery
                .OrderByDescending(p => p.Id)
                .Select(p => new ProductInfoServiceModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Dimensions = p.Dimensions,
                    InPallet = $"{p.QuantityInPalletInPieces} pieces / {p.QuantityInPalletInUnitOfMeasurement}{p.UnitOfMeasurement}",
                    CategoryName = p.Category.Name,
                    DefaultImageUrl = p.ImageUrl
                })
                .ToList();

            return products;
        }

        public ProductDetailsServiceModel GetProductDetails(int id)
            => this.data.Products
                .Where(p => p.Id == id)
                .Select(p => new ProductDetailsServiceModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Dimensions = p.Dimensions,
                    QuantityInPalletInUnitOfMeasurement = p.QuantityInPalletInUnitOfMeasurement,
                    QuantityInPalletInPieces = p.QuantityInPalletInPieces,
                    CountInUnitOfMeasurement = p.CountInUnitOfMeasurement,
                    UnitOfMeasurement = p.UnitOfMeasurement.ToString(),
                    Weight = p.Weight,
                    CategoryName = p.Category.Name,
                    DefaultImageUrl = p.ImageUrl,
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

        public void AddColorToProduct(int productId, int colorId)
        {
            var product = this.data.Products.Find(productId);

            product.ProductColors.Add(new ProductColor
            {
                ColorId = colorId
            });

            this.data.SaveChanges();
        }

        public bool IsProductExist(int id)
            => this.data.Products.Any(p => p.Id == id);

        public bool HasProductWithSameNameAndDimensions(string name, string dimensions)
            => this.data.Products
                .Any(p => p.Name == name && p.Dimensions == dimensions);

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
