namespace ConcreteProducts.Web.Services.Products
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;

    using ConcreteProducts.Data;
    using ConcreteProducts.Data.Models;
    using ConcreteProducts.Data.Models.Enumerations;
    using ConcreteProducts.Web.Services.Products.Models;

    public class ProductService : IProductService
    {
        private readonly ConcreteProductsDbContext data;
        private readonly IMapper mapper;

        public ProductService(ConcreteProductsDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ProductListingServiceModel>> GetAllListingProductsAsync(string searchTerm)
        {
            var productQuery = this.data.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productQuery = productQuery.Where(p => p.Name.Contains(searchTerm) || p.Dimensions.Contains(searchTerm));
            }

            var products = await productQuery
                .OrderByDescending(p => p.Id)
                .ProjectTo<ProductListingServiceModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

            return products;
        }

        public async Task<List<ProductListingServiceModel>> GetLatestProductsAsync()
            => await this.data.Products
                .OrderByDescending(i => i.Id)
                .ProjectTo<ProductListingServiceModel>(this.mapper.ConfigurationProvider)
                .Take(6)
                .ToListAsync();

        public async Task<ProductDetailsServiceModel> GetProductDetailsAsync(int id)
            => await this.data.Products
                .Where(p => p.Id == id)
                .ProjectTo<ProductDetailsServiceModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

        public async Task<ProductBaseServiceModel> GetProductToDeleteByIdAsync(int id)
            => await this.data.Products
                .Where(p => p.Id == id)
                .ProjectTo<ProductBaseServiceModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

        public async Task<int> CreateAsync(
            string name,
            string dimensions,
            double quantityInPalletInUnitOfMeasurement,
            double quantityInPalletInPieces,
            double countInUnitOfMeasurement,
            UnitOfMeasurement unitOfMeasurement,
            double weight,
            string imageUrl,
            int categoryId,
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
            };

            product.ProductColors.Add(new ProductColor
            {
                ColorId = colorId
            });


            await this.data.Products.AddAsync(product);
            await this.data.SaveChangesAsync();

            return product.Id;
        }

        public async Task<bool> IsProductExistAsync(int id)
            => await this.data.Products.AnyAsync(p => p.Id == id);

        public async Task<bool> HasProductWithSameNameAndDimensionsAsync(string name, string dimensions)
            => await this.data.Products
                .AnyAsync(p => p.Name == name && p.Dimensions == dimensions);

        public async Task DeleteProductAsync(int id)
        {
            var product = await this.data.Products
                .Include(p => p.ProductColors)
                .FirstOrDefaultAsync(p => p.Id == id);

            var colorsRelatedToProduct = await this.data.ProductColors
                .Where(c => c.Product == product)
                .Select(c => c.ProductColorId)
                .ToListAsync();

            var warehouseProductColors = await this.data.WarehouseProductColors
                .Where(c => colorsRelatedToProduct.Contains(c.ProductColorId))
                .ToListAsync();


            this.data.WarehouseProductColors.RemoveRange(warehouseProductColors);
            this.data.ProductColors.RemoveRange(product.ProductColors);
            this.data.Products.Remove(product);
            await this.data.SaveChangesAsync();
        }

    }
}
