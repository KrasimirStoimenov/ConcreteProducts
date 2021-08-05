namespace ConcreteProducts.Web.Services.WarehouseProducts
{
    public interface IWarehouseProductService
    {
        void AddProductToWarehouse(int productId, int warehouseId, int count);
    }
}
