namespace ConcreteProducts.Services.Shapes
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using ConcreteProducts.Data;
    using ConcreteProducts.Data.Models;
    using ConcreteProducts.Services.Shapes.Models;
    using Microsoft.EntityFrameworkCore;

    public class ShapeService : IShapeService
    {
        private readonly ConcreteProductsDbContext data;
        private readonly IMapper mapper;

        public ShapeService(ConcreteProductsDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ShapeBaseServiceModel>> GetAllShapesAsync()
                => await this.data.Shapes
                    .ProjectTo<ShapeBaseServiceModel>(this.mapper.ConfigurationProvider)
                    .OrderBy(s => s.Id)
                    .ToListAsync();

        public async Task<IEnumerable<ShapeAndWarehouseServiceModel>> GetAllShapesWithWarehouseAsync()
            => await this.data.Shapes
                .ProjectTo<ShapeAndWarehouseServiceModel>(this.mapper.ConfigurationProvider)
                .OrderBy(s => s.Id)
                .ToListAsync();

        public async Task<ShapeBaseServiceModel> GetShapeToDeleteByIdAsync(int id)
            => await this.data.Shapes
                .Where(s => s.Id == id)
                .ProjectTo<ShapeBaseServiceModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

        public async Task<int> CreateAsync(string name, string dimensions, int warehouseId)
        {
            var shape = new Shape
            {
                Name = name,
                Dimensions = dimensions,
                WarehouseId = warehouseId,
            };

            await this.data.Shapes.AddAsync(shape);
            await this.data.SaveChangesAsync();

            return shape.Id;
        }

        public async Task EditAsync(int id, string name, string dimensions, int warehouseId)
        {
            var shape = await this.data.Shapes.FindAsync(id);

            shape.Name = name;
            shape.Dimensions = dimensions;
            shape.WarehouseId = warehouseId;

            await this.data.SaveChangesAsync();
        }

        public async Task<ShapeDetailsServiceModel> GetShapeDetailsAsync(int id)
                => await this.data.Shapes
                    .Where(s => s.Id == id)
                    .ProjectTo<ShapeDetailsServiceModel>(this.mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();

        public async Task<bool> IsShapeExistAsync(int id)
            => await this.data.Shapes.AnyAsync(s => s.Id == id);

        public async Task<bool> HasShapeWithSameNameAsync(string name)
            => await this.data.Shapes
                .AnyAsync(s => s.Name == name);

        public async Task DeleteShapeAsync(int id)
        {
            var shape = await this.data.Shapes.FindAsync(id);

            this.data.Shapes.Remove(shape);
            await this.data.SaveChangesAsync();
        }
    }
}
