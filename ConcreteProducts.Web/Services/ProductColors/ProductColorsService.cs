namespace ConcreteProducts.Web.Services.ProductColors
{
    using System.Linq;
    using System.Collections.Generic;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using ConcreteProducts.Web.Data;
    using ConcreteProducts.Web.Data.Models;
    using ConcreteProducts.Web.Services.Colors.Models;

    public class ProductColorsService : IProductColorsService
    {
        private readonly ConcreteProductsDbContext data;
        private readonly IMapper mapper;

        public ProductColorsService(ConcreteProductsDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public void AddColorToProduct(int productId, int colorId)
        {
            var product = this.data.Products.Find(productId);

            product.ProductColors.Add(new ProductColor
            {
                ColorId = colorId
            });

            this.data.SaveChanges();
        }

        public IEnumerable<ColorBaseServiceModel> GetColorsNotRelatedToProduct(int productId)
        {
            var productColors = this.data.ProductColors
                .Where(c => c.ProductId == productId)
                .Select(c => c.ColorId)
                .ToList();

            var allColors = this.data.Colors.ToList();

            var notRelatedColor = this.data.Colors
                .Where(c => !productColors.Contains(c.Id))
                .ProjectTo<ColorBaseServiceModel>(this.mapper.ConfigurationProvider)
                .ToList();

            return notRelatedColor;

        }

        public bool IsColorRelatedToProduct(int productId, int colorId)
            => this.data.ProductColors
                .Where(p => p.ProductId == productId)
                .Any(c => c.ColorId == colorId);
    }
}
