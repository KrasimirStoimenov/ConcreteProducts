namespace ConcreteProducts.Services.ProductColors
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using ConcreteProducts.Data;
    using ConcreteProducts.Data.Models;
    using ConcreteProducts.Services.Colors.Models;
    using ConcreteProducts.Services.ProductColors.Model;
    using Microsoft.EntityFrameworkCore;

    public class ProductColorsService : IProductColorsService
    {
        private readonly ConcreteProductsDbContext data;
        private readonly IMapper mapper;

        public ProductColorsService(ConcreteProductsDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ProductColorBaseServiceModel>> GetAllProductColorsAsync()
            => await this.data.ProductColors
                .ProjectTo<ProductColorBaseServiceModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

        public async Task AddColorToProductAsync(int productId, int colorId)
        {
            var product = await this.data.Products.FindAsync(productId);

            product.ProductColors.Add(new ProductColor
            {
                ColorId = colorId,
            });

            await this.data.SaveChangesAsync();
        }

        public async Task<IEnumerable<ColorBaseServiceModel>> GetColorsNotRelatedToProductAsync(int productId)
        {
            var productColors = await this.data.ProductColors
                .Where(c => c.ProductId == productId)
                .Select(c => c.ColorId)
                .ToListAsync();

            var allColors = await this.data.Colors.ToListAsync();

            var notRelatedColor = await this.data.Colors
                .Where(c => !productColors.Contains(c.Id))
                .ProjectTo<ColorBaseServiceModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

            return notRelatedColor;
        }

        public async Task<bool> IsColorRelatedToProductAsync(int productId, int colorId)
            => await this.data.ProductColors
                .Where(p => p.ProductId == productId)
                .AnyAsync(c => c.ColorId == colorId);

        public async Task<bool> IsProductColorExistAsync(int productColorId)
            => await this.data.ProductColors
                .AnyAsync(pc => pc.ProductColorId == productColorId);
    }
}
