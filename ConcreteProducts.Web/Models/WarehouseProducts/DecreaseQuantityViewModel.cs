namespace ConcreteProducts.Web.Models.WarehouseProducts
{
    public class DecreaseQuantityViewModel
    {
        public int ProductColorId { get; init; }

        public string ProductName { get; init; }

        public int WarehouseId { get; init; }

        public int Count { get; init; }
    }
}
