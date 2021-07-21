namespace ConcreteProducts.Web.Services.Warehouses
{
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using ConcreteProducts.Web.Data;
    using ConcreteProducts.Web.Services.Warehouses.Dtos;

    public class WarehouseService : IWarehouseService
    {
        private readonly ConcreteProductsDbContext data;

        public WarehouseService(ConcreteProductsDbContext data)
            => this.data = data;

        public IEnumerable<WarehouseServiceModel> GetAllWarehouses()
            => this.data
                .Warehouses
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

        public bool IsWarehouseExist(int id)
            => this.data.Warehouses.Any(w => w.Id == id);

        public void DeleteWarehouse(int id)
        {
            var warehouse = this.data.Warehouses
                .Include(w => w.Products)
                .Include(w => w.Shapes)
                .FirstOrDefault(c => c.Id == id);

            if (warehouse.Products.Any() || warehouse.Shapes.Any())
            {
                return;
            }

            this.data.Warehouses.Remove(warehouse);
            this.data.SaveChanges();
        }
    }
}
