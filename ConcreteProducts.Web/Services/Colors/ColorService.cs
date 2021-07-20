namespace ConcreteProducts.Web.Services.Colors
{
    using System.Linq;
    using System.Collections.Generic;
    using ConcreteProducts.Web.Data;
    using ConcreteProducts.Web.Services.Colors.Dtos;

    public class ColorService : IColorService
    {
        private readonly ConcreteProductsDbContext data;

        public ColorService(ConcreteProductsDbContext data)
            => this.data = data;

        public IEnumerable<ColorServiceModel> GetAllColors()
            => this.data.Colors
                .Select(c => new ColorServiceModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .OrderBy(c => c.Id)
                .ToList();
    }
}
