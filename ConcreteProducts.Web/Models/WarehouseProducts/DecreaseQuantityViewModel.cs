namespace ConcreteProducts.Web.Models.WarehouseProducts
{
    using System.ComponentModel.DataAnnotations;

    using ConcreteProducts.Web.Infrastructure.ValidationAttributes;

    public class DecreaseQuantityViewModel
    {
        [IsValidProductColorId]
        public int ProductColorId { get; init; }

        public string ProductName { get; init; }

        [IsValidWarehouseId]
        public int WarehouseId { get; init; }

        public int Count { get; init; }
    }
}
