namespace ConcreteProducts.Web.Services.WarehouseProducts
{
    using System.Linq;
    using System.Collections.Generic;
    using ConcreteProducts.Web.Data.Models;
    using ConcreteProducts.Web.Data;
    using ConcreteProducts.Web.Services.Products;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using ConcreteProducts.Web.Services.WarehouseProducts.Models;
    using static ConcreteProducts.Web.Data.DataConstants;

    public class WarehouseProductService : IWarehouseProductService
    {
        private readonly ConcreteProductsDbContext data;
        private readonly IMapper mapper;

        public WarehouseProductService(ConcreteProductsDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public void AddProductToWarehouse(int productColorId, int warehouseId, int count)
        {
            if (!this.data.WarehouseProductColors.Any(c => c.ProductColorId == productColorId && c.WarehouseId == warehouseId))
            {
                var warehouseProduct = new WarehouseProductColors
                {
                    ProductColorId = productColorId,
                    WarehouseId = warehouseId,
                    Count = count
                };

                this.data.WarehouseProductColors.Add(warehouseProduct);
            }
            else
            {
                var warehouseProducts = this.GetProductInWarehouse(productColorId, warehouseId);
                warehouseProducts.Count += count;
            }

            this.data.SaveChanges();
        }

        public int AvailableQuantity(int productColorId, int warehouseId)
        {
            var warehouse = this.GetProductInWarehouse(productColorId, warehouseId);

            return warehouse.Count;
        }

        public void DecreaseQuantityFromProductsInWarehouse(int productColorId, int warehouseId, int count)
        {
            var warehouseProducts = this.GetProductInWarehouse(productColorId, warehouseId);
            warehouseProducts.Count -= count;

            this.data.SaveChanges();
        }

        public IEnumerable<WarehouseProductsServiceModel> GetAllProductsInWarehouse()
            => this.data.WarehouseProductColors
                .Where(wp => wp.Count > 0)
                .OrderBy(wp => wp.ProductColor.Product.Name)
                .ThenBy(w => w.Warehouse.Name)
                .ProjectTo<WarehouseProductsServiceModel>(this.mapper.ConfigurationProvider)
                .ToList();

        private WarehouseProductColors GetProductInWarehouse(int productColorId, int warehouseId)
            => this.data.WarehouseProductColors
                    .FirstOrDefault(wp => wp.ProductColorId == productColorId && wp.WarehouseId == warehouseId);
    }
}
