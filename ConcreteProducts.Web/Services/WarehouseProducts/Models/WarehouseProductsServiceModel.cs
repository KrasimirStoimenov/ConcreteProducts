namespace ConcreteProducts.Web.Services.WarehouseProducts.Models
{
    public class WarehouseProductsServiceModel
    {
        public int ProductColorId { get; init; }

        public string ProductColorName { get; init; }

        public string WarehouseName { get; init; }

        public int Count { get; init; }

        public string TotalUnitOfMeasurement { get; init; }

        public int Pallets { get; init; }
    }
}
