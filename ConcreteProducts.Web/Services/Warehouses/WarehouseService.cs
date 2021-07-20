namespace ConcreteProducts.Web.Services.Warehouses
{
    using System.Linq;
    using System.Collections.Generic;
    using ConcreteProducts.Web.Data;
    using ConcreteProducts.Web.Services.Warehouses.Dtos;
    using ConcreteProducts.Web.Models.Warehouses;

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
    }
}
