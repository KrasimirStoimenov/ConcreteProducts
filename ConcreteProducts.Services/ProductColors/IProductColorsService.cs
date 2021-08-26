namespace ConcreteProducts.Services.ProductColors
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using ConcreteProducts.Services.Colors.Models;
    using ConcreteProducts.Services.ProductColors.Model;

    public interface IProductColorsService
    {
        Task<IEnumerable<ProductColorBaseServiceModel>> GetAllProductColorsAsync();

        Task AddColorToProductAsync(int productId, int colorId);

        Task<IEnumerable<ColorBaseServiceModel>> GetColorsNotRelatedToProductAsync(int productId);

        Task<bool> IsColorRelatedToProductAsync(int productId, int colorId);

        Task<bool> IsProductColorExistAsync(int productColorId);
    }
}
