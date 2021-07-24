namespace ConcreteProducts.Web.Services.Shapes
{
    using System.Linq;
    using System.Collections.Generic;
    using ConcreteProducts.Web.Data;
    using ConcreteProducts.Web.Services.Shapes.Dtos;
    using ConcreteProducts.Web.Data.Models;

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

        public ShapeServiceModel GetShapeToDeleteById(int id)
            => this.data.Shapes
                .Where(s => s.Id == id)
                .Select(s => new ShapeServiceModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Dimensions = s.Dimensions
                })
.FirstOrDefault();
        public int Create(string name, string dimensions, int warehouseId)
        {
            var shape = new Shape
            {
                Name = name,
                Dimensions = dimensions,
                WarehouseId = warehouseId
            };

            this.data.Shapes.Add(shape);
            this.data.SaveChanges();

            return shape.Id;
        }

        public void Edit(int id, string name, string dimensions, int warehouseId)
        {
            var shape = this.data.Shapes.Find(id);

            shape.Name = name;
            shape.Dimensions = dimensions;
            shape.WarehouseId = warehouseId;

            this.data.SaveChanges();
        }

        public ShapeDetailsServiceModel GetShapeDetails(int id)
            => this.data.Shapes
                .Where(s => s.Id == id)
                .Select(s => new ShapeDetailsServiceModel
                {
                    Name = s.Name,
                    Dimensions = s.Dimensions,
                    WarehouseId = s.WarehouseId
                })
                .FirstOrDefault();

        public bool IsShapeExist(int id)
            => this.data.Shapes.Any(s => s.Id == id);

        public bool HasShapeWithSameName(string name)
            => this.data.Shapes
                .Any(s => s.Name == name);

        public void DeleteShape(int id)
        {
            var shape = this.data.Shapes.Find(id);

            this.data.Shapes.Remove(shape);
            this.data.SaveChanges();
        }
    }
}
