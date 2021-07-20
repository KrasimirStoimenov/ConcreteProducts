namespace ConcreteProducts.Web.Services.Warehouses.Dtos
{
    public class WarehouseWithProductsAndShapesCount : WarehouseServiceModel
    {
        public int TotalProductsCount { get; init; }

        public int TotalShapesCount { get; init; }
    }
}
