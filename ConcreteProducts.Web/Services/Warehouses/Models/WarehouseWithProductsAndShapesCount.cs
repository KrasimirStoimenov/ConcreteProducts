namespace ConcreteProducts.Web.Services.Warehouses.Models
{
    public class WarehouseWithProductsAndShapesCount : WarehouseServiceModel
    {
        public int TotalProductsCount { get; init; }

        public int TotalShapesCount { get; init; }
    }
}
