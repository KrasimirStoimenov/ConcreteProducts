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

        public ColorDeleteServiceModel GetColorToDeleteById(int id)
            => this.data.Colors
                .Where(c => c.Id == id)
                .Select(c => new ColorDeleteServiceModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    ProductsRelatedToColor = c.ProductColors.Count()
                })
                .FirstOrDefault();

        public bool IsColorExist(int id)
            => this.data.Colors.Any(c => c.Id == id);

        public void DeleteColor(int id)
        {
            var color = this.data.Colors.Find(id);

            this.data.Colors.Remove(color);
            this.data.SaveChanges();
        }
    }
}
