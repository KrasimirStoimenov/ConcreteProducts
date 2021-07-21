using System.Collections.Generic;
using ConcreteProducts.Web.Services.Colors.Dtos;

namespace ConcreteProducts.Web.Services.Colors
{
    public interface IColorService
    {
        IEnumerable<ColorServiceModel> GetAllColors();

        bool IsColorExist(int id);

        void DeleteColor(int id);
    }
}
