namespace ConcreteProducts.Data.Models
{
    public class WarehouseProductColors
    {
        public int WarehouseId { get; set; }

        public Warehouse Warehouse { get; init; }

        public int ProductColorId { get; set; }

        public ProductColor ProductColor { get; init; }

        public int Count { get; set; }
    }
}
