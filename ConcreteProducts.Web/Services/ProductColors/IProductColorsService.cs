namespace ConcreteProducts.Web.Services.ProductColors
{
    using System.Collections.Generic;
    using ConcreteProducts.Web.Services.Colors.Models;

    public interface IProductColorsService
    {
        void AddColorToProduct(int productId, int colorId);

        IEnumerable<ColorServiceModel> GetColorsNotRelatedToProduct(int productId);

        bool IsColorRelatedToProduct(int productId,int colorId);
    }
}
