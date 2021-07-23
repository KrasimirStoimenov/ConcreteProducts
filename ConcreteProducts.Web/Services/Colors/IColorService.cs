namespace ConcreteProducts.Web.Services.Colors
{
    using System.Collections.Generic;
    using ConcreteProducts.Web.Services.Colors.Dtos;

    public interface IColorService
    {
        IEnumerable<ColorServiceModel> GetAllColors();

        ColorDeleteServiceModel GetColorToDeleteById(int id);

        bool IsColorExist(int id);

        void DeleteColor(int id);
    }
}
