namespace ConcreteProducts.Web.Services.WarehouseProducts
{
    using System.Linq;
    using ConcreteProducts.Web.Data.Models;
    using ConcreteProducts.Web.Data;
    using ConcreteProducts.Web.Services.Products;

    public class WarehouseProductService : IWarehouseProductService
    {
        private readonly ConcreteProductsDbContext data;

        public WarehouseProductService(ConcreteProductsDbContext data)
        {
            this.data = data;
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
                var warehouseProducts = this.data.WarehouseProductColors
                        .FirstOrDefault(wp => wp.ProductColorId == productColorId && wp.WarehouseId == warehouseId);

                warehouseProducts.Count += count;
            }

            this.data.SaveChanges();
        }
    }
}
