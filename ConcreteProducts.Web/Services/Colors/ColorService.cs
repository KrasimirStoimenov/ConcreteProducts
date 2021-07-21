namespace ConcreteProducts.Web.Services.Colors
{
    using System.Linq;
    using System.Collections.Generic;
    using ConcreteProducts.Web.Data;
    using ConcreteProducts.Web.Services.Colors.Dtos;
    using Microsoft.EntityFrameworkCore;

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

        public bool IsColorExist(int id)
            => this.data.Colors.Any(c => c.Id == id);

        public void DeleteColor(int id)
        {
            var color = this.data.Colors.Include(pc => pc.ProductColors).FirstOrDefault(c => c.Id == id);

            if (color.ProductColors.Any())
            {
                return;
            }

            this.data.Colors.Remove(color);
            this.data.SaveChanges();
        }
    }
}
