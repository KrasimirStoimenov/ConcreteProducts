namespace ConcreteProducts.Web.Services.Warehouses
{
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using ConcreteProducts.Data;
    using ConcreteProducts.Data.Models;
    using ConcreteProducts.Web.Services.Warehouses.Models;

    public class WarehouseService : IWarehouseService
    {
        private readonly ConcreteProductsDbContext data;
        private readonly IMapper mapper;

        public WarehouseService(ConcreteProductsDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public IEnumerable<WarehouseBaseServiceModel> GetAllWarehouses()
            => this.data.Warehouses
                .ProjectTo<WarehouseBaseServiceModel>(this.mapper.ConfigurationProvider)
                .OrderBy(w => w.Id)
                .ToList();

        public IEnumerable<WarehouseWithProductsAndShapesCount> GetWarehousesWithProductsAndShapesCount()
            => this.data.Warehouses
                .ProjectTo<WarehouseWithProductsAndShapesCount>(this.mapper.ConfigurationProvider)
                .OrderBy(w => w.Id)
                .ToList();

        public WarehouseWithProductsAndShapesCount GetWarehouseToDeleteById(int id)
            => this.data.Warehouses
                .Where(w => w.Id == id)
                .ProjectTo<WarehouseWithProductsAndShapesCount>(this.mapper.ConfigurationProvider)
                .FirstOrDefault();

        public int Create(string name)
        {
            var warehouse = new Warehouse
            {
                Name = name
            };

            this.data.Warehouses.Add(warehouse);
            this.data.SaveChanges();

            return warehouse.Id;
        }

        public void Edit(int id, string name)
        {
            var warehouse = this.data.Warehouses.Find(id);

            warehouse.Name = name;

            this.data.SaveChanges();
        }

        public WarehouseBaseServiceModel GetWarehouseDetails(int id)
            => this.data.Warehouses
                .Where(w => w.Id == id)
                .ProjectTo<WarehouseBaseServiceModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefault();

        public bool IsWarehouseExist(int id)
            => this.data.Warehouses.Any(w => w.Id == id);

        public bool HasWarehouseWithSameName(string name)
            => this.data.Warehouses
                .Any(w => w.Name == name);

        public void DeleteWarehouse(int id)
        {
            var warehouse = this.data.Warehouses.Find(id);

            this.data.Warehouses.Remove(warehouse);
            this.data.SaveChanges();
        }
    }
}
