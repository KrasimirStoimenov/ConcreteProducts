namespace ConcreteProducts.Web.Services.Shapes
{
    using System.Linq;
    using System.Collections.Generic;
    using ConcreteProducts.Web.Data;
    using ConcreteProducts.Web.Services.Shapes.Dto;
using Microsoft.EntityFrameworkCore;

    public class ShapeService : IShapeService
    {
        private readonly ConcreteProductsDbContext data;

        public ShapeService(ConcreteProductsDbContext data)
            => this.data = data;

        public IEnumerable<ShapeServiceModel> GetAllShapes()
            => this.data.Shapes
                .Select(s => new ShapeServiceModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Dimensions = s.Dimensions,
                })
                .OrderBy(s => s.Id)
                .ToList();

        public IEnumerable<ShapeAndWarehouseServiceModel> GetAllShapesWithWarehouse()
            => this.data.Shapes
                .Select(s => new ShapeAndWarehouseServiceModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Dimensions = s.Dimensions,
                    WarehouseName = s.Warehouse.Name
                })
                .OrderBy(s => s.Id)
                .ToList();

        public bool IsShapeExist(int id)
            => this.data.Shapes.Any(s => s.Id == id);

        public void DeleteShape(int id)
        {
            var shape = this.data.Shapes.Find(id);

            this.data.Shapes.Remove(shape);
            this.data.SaveChanges();
        }
    }
}
