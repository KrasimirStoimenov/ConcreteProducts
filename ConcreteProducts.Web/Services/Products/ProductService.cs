namespace ConcreteProducts.Web.Services.Products
{
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using ConcreteProducts.Web.Data;
    using ConcreteProducts.Web.Data.Models;
    using ConcreteProducts.Web.Services.Products.Dtos;
    using ConcreteProducts.Web.Data.Models.Enumerations;

    public class ProductService : IProductService
    {
        private readonly ConcreteProductsDbContext data;
        private readonly IMapper mapper;

        public ProductService(ConcreteProductsDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public IEnumerable<ProductListingServiceModel> GetAllListingProducts(string searchTerm)
        {
            var productQuery = this.data.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productQuery = productQuery.Where(p => p.Name.Contains(searchTerm) || p.Dimensions.Contains(searchTerm));
            }

            var products = productQuery
                .OrderByDescending(p => p.Id)
                .ProjectTo<ProductListingServiceModel>(this.mapper.ConfigurationProvider)
                .ToList();

            return products;
        }

        public ProductDetailsServiceModel GetProductDetails(int id)
            => this.data.Products
                .Where(p => p.Id == id)
                .ProjectTo<ProductDetailsServiceModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefault();

        public ProductBaseServiceModel GetProductToDeleteById(int id)
            => this.data.Products
                .Where(p => p.Id == id)
                .ProjectTo<ProductBaseServiceModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefault();

        public int Create(
            string name,
            string dimensions,
            double quantityInPalletInUnitOfMeasurement,
            double quantityInPalletInPieces,
            double countInUnitOfMeasurement,
            UnitOfMeasurement unitOfMeasurement,
            double weight,
            string imageUrl,
            int categoryId,
            int warehouseId,
            int colorId)
        {
            var product = new Product
            {
                Name = name,
                Dimensions = dimensions,
                QuantityInPalletInUnitOfMeasurement = quantityInPalletInUnitOfMeasurement,
                QuantityInPalletInPieces = quantityInPalletInPieces,
                CountInUnitOfMeasurement = countInUnitOfMeasurement,
                UnitOfMeasurement = unitOfMeasurement,
                Weight = weight,
                ImageUrl = imageUrl,
                CategoryId = categoryId,
                WarehouseId = warehouseId
            };

            product.ProductColors.Add(new ProductColor
            {
                ColorId = colorId
            });


            this.data.Products.Add(product);
            this.data.SaveChanges();

            return product.Id;
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
