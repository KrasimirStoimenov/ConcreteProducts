namespace ConcreteProducts.Web.Data.Models
{
    public class WarehouseProducts
    {
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; init; }

        public int ProductId { get; set; }
        public Product Product { get; init; }

        public int Count { get; set; }
    }
}
