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

        public void AddProductToWarehouse(int productId, int warehouseId, int count)
        {
            if (!this.data.WarehouseProducts.Any(c => c.ProductId == productId && c.WarehouseId == warehouseId))
            {
                var warehouseProduct = new WarehouseProducts
                {
                    ProductId = productId,
                    WarehouseId = warehouseId,
                    Count = count
                };

                this.data.WarehouseProducts.Add(warehouseProduct);
            }
            else
            {
                var warehouseProducts = this.data.WarehouseProducts
                        .FirstOrDefault(wp => wp.ProductId == productId && wp.WarehouseId == warehouseId);

                warehouseProducts.Count += count;
            }

            this.data.SaveChanges();
        }
    }
}
