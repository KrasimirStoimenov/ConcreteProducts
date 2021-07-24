namespace ConcreteProducts.Web.Services.Warehouses
{
    using System.Linq;
    using System.Collections.Generic;
    using ConcreteProducts.Web.Data;
    using ConcreteProducts.Web.Services.Warehouses.Dtos;
    using ConcreteProducts.Web.Data.Models;

    public class WarehouseService : IWarehouseService
    {
        private readonly ConcreteProductsDbContext data;

        public WarehouseService(ConcreteProductsDbContext data)
            => this.data = data;

        public IEnumerable<WarehouseServiceModel> GetAllWarehouses()
            => this.data.Warehouses
                .Select(w => new WarehouseServiceModel
                {
                    Id = w.Id,
                    Name = w.Name
                })
                .OrderBy(w => w.Id)
                .ToList();

        public IEnumerable<WarehouseWithProductsAndShapesCount> GetWarehousesWithProductsAndShapesCount()
            => this.data.Warehouses
                .Select(w => new WarehouseWithProductsAndShapesCount
                {
                    Id = w.Id,
                    Name = w.Name,
                    TotalProductsCount = w.Products.Count,
                    TotalShapesCount = w.Shapes.Count
                })
                .OrderBy(w => w.Id)
                .ToList();

        public WarehouseWithProductsAndShapesCount GetWarehouseToDeleteById(int id)
            => this.data.Warehouses
                .Select(w => new WarehouseWithProductsAndShapesCount
                {
                    Id = w.Id,
                    Name = w.Name,
                    TotalProductsCount = w.Products.Count,
                    TotalShapesCount = w.Shapes.Count
                })
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

        public WarehouseServiceModel GetWarehouseDetails(int id)
            => this.data.Warehouses
                .Where(w => w.Id == id)
                .Select(w => new WarehouseServiceModel
                {
                    Name = w.Name
                })
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
