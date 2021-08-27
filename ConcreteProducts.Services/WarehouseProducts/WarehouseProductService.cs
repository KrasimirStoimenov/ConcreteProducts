namespace ConcreteProducts.Services.WarehouseProducts
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;

    using ConcreteProducts.Data;
    using ConcreteProducts.Data.Models;
    using ConcreteProducts.Services.WarehouseProducts.Models;

    using static Common.DataAttributeConstants;

    public class WarehouseProductService : IWarehouseProductService
    {
        private readonly ConcreteProductsDbContext data;
        private readonly IMapper mapper;

        public WarehouseProductService(ConcreteProductsDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public async Task AddProductToWarehouseAsync(int productColorId, int warehouseId, int count)
        {
            if (!await this.data.WarehouseProductColors.AnyAsync(c => c.ProductColorId == productColorId && c.WarehouseId == warehouseId))
            {
                var warehouseProduct = new WarehouseProductColors
                {
                    ProductColorId = productColorId,
                    WarehouseId = warehouseId,
                    Count = count
                };

                await this.data.WarehouseProductColors.AddAsync(warehouseProduct);
            }
            else
            {
                var warehouseProducts = await this.GetProductInWarehouse(productColorId, warehouseId);
                warehouseProducts.Count += count;
            }

            this.data.SaveChanges();
        }

        public async Task<int> AvailableQuantityAsync(int productColorId, int warehouseId)
        {
            var warehouse = await this.GetProductInWarehouse(productColorId, warehouseId);

            return warehouse.Count;
        }

        public async Task DecreaseQuantityFromProductsInWarehouseAsync(int productColorId, int warehouseId, int count)
        {
            var warehouseProducts = await this.GetProductInWarehouse(productColorId, warehouseId);
            warehouseProducts.Count -= count;

            this.data.SaveChanges();
        }

        public async Task<IEnumerable<WarehouseProductsServiceModel>> GetAllProductsInWarehouseAsync()
            => await this.data.WarehouseProductColors
                .Where(wp => wp.Count > 0)
                .OrderBy(wp => wp.ProductColor.Product.Name)
                .ThenBy(w => w.Warehouse.Name)
                .ProjectTo<WarehouseProductsServiceModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

        private async Task<WarehouseProductColors> GetProductInWarehouse(int productColorId, int warehouseId)
            => await this.data.WarehouseProductColors
                    .FirstOrDefaultAsync(wp => wp.ProductColorId == productColorId && wp.WarehouseId == warehouseId);
    }
}
