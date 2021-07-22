namespace ConcreteProducts.Web.Services.Products
{
    using System.Collections.Generic;
    using System.Linq;
    using ConcreteProducts.Web.Data;
    using ConcreteProducts.Web.Services.Products.Dtos;

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

        public bool IsProductExist(int id)
            => this.data.Products.Any(p => p.Id == id);

        public void DeleteProduct(int id)
        {
            var product = this.data.Products.Find(id);

            this.data.Products.Remove(product);
            this.data.SaveChanges();
        }

    }
}
