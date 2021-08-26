namespace ConcreteProducts.Services.ProductColors
{
    using System.Collections.Generic;
    using ConcreteProducts.Services.Colors.Models;
    using ConcreteProducts.Services.ProductColors.Model;

    public interface IProductColorsService
    {
        IEnumerable<ProductColorBaseServiceModel> GetAllProductColors();

        void AddColorToProduct(int productId, int colorId);

        IEnumerable<ColorBaseServiceModel> GetColorsNotRelatedToProduct(int productId);

        bool IsColorRelatedToProduct(int productId, int colorId);

        bool IsProductColorExist(int productColorId);
    }
}
