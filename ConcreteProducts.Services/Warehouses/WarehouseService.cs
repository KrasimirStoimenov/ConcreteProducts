namespace ConcreteProducts.Services.Warehouses
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using ConcreteProducts.Data;
    using ConcreteProducts.Data.Models;
    using ConcreteProducts.Services.Warehouses.Models;
    using Microsoft.EntityFrameworkCore;

    public class WarehouseService : IWarehouseService
    {
        private readonly ConcreteProductsDbContext data;
        private readonly IMapper mapper;

        public WarehouseService(ConcreteProductsDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<WarehouseBaseServiceModel>> GetAllWarehousesAsync()
            => await this.data.Warehouses
                .ProjectTo<WarehouseBaseServiceModel>(this.mapper.ConfigurationProvider)
                .OrderBy(w => w.Id)
                .ToListAsync();

        public async Task<IEnumerable<WarehouseWithProductsAndShapesCount>> GetWarehousesWithProductsAndShapesCountAsync()
            => await this.data.Warehouses
                .ProjectTo<WarehouseWithProductsAndShapesCount>(this.mapper.ConfigurationProvider)
                .OrderBy(w => w.Id)
                .ToListAsync();

        public async Task<WarehouseWithProductsAndShapesCount> GetWarehouseToDeleteByIdAsync(int id)
            => await this.data.Warehouses
                .Where(w => w.Id == id)
                .ProjectTo<WarehouseWithProductsAndShapesCount>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

        public async Task<int> CreateAsync(string name)
        {
            var warehouse = new Warehouse
            {
                Name = name,
            };

            await this.data.Warehouses.AddAsync(warehouse);
            await this.data.SaveChangesAsync();

            return warehouse.Id;
        }

        public async Task EditAsync(int id, string name)
        {
            var warehouse = await this.data.Warehouses.FindAsync(id);

            warehouse.Name = name;

            await this.data.SaveChangesAsync();
        }

        public async Task<WarehouseBaseServiceModel> GetWarehouseDetailsAsync(int id)
            => await this.data.Warehouses
                .Where(w => w.Id == id)
                .ProjectTo<WarehouseBaseServiceModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

        public async Task<bool> IsWarehouseExistAsync(int id)
            => await this.data.Warehouses
                .AnyAsync(w => w.Id == id);

        public async Task<bool> HasWarehouseWithSameNameAsync(string name)
            => await this.data.Warehouses
                .AnyAsync(w => w.Name == name);

        public async Task DeleteWarehouseAsync(int id)
        {
            var warehouse = await this.data.Warehouses.FindAsync(id);

            this.data.Warehouses.Remove(warehouse);
            await this.data.SaveChangesAsync();
        }
    }
}
