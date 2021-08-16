namespace ConcreteProducts.Web.Services.Shapes
{
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using ConcreteProducts.Data;
    using ConcreteProducts.Data.Models;
    using ConcreteProducts.Web.Services.Shapes.Models;

    public class ShapeService : IShapeService
    {
        private readonly ConcreteProductsDbContext data;
        private readonly IMapper mapper;

        public ShapeService(ConcreteProductsDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public IEnumerable<ShapeBaseServiceModel> GetAllShapes()
            => this.data.Shapes
                .ProjectTo<ShapeBaseServiceModel>(this.mapper.ConfigurationProvider)
                .OrderBy(s => s.Id)
                .ToList();

        public IEnumerable<ShapeAndWarehouseServiceModel> GetAllShapesWithWarehouse()
            => this.data.Shapes
                .ProjectTo<ShapeAndWarehouseServiceModel>(this.mapper.ConfigurationProvider)
                .OrderBy(s => s.Id)
                .ToList();

        public ShapeBaseServiceModel GetShapeToDeleteById(int id)
            => this.data.Shapes
                .Where(s => s.Id == id)
                .ProjectTo<ShapeBaseServiceModel>(this.mapper.ConfigurationProvider)
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
                .ProjectTo<ShapeDetailsServiceModel>(this.mapper.ConfigurationProvider)
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
