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

        public IEnumerable<ProductServiceModel> GetAllProducts()
            => this.data.Products
                .OrderByDescending(p => p.Id)
                .Select(p => new ProductServiceModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Dimensions = p.Dimensions,
                    InPallet = $"{p.QuantityInPalletInPieces} pieces / {p.QuantityInPalletInUnitOfMeasurement}{p.UnitOfMeasurement}",
                    DefaultImageUrl = p.ProductColors
                                        .Where(c => c.Color.Name == "Grey")
                                        .Select(pc => pc.ImageUrl)
                                        .FirstOrDefault(),
                })
                .ToList();
    }
}
