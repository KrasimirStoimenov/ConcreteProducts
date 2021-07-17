namespace ConcreteProducts.Web.Models.Warehouses
{
    using System.ComponentModel.DataAnnotations;

    public class WarehouseListingViewModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public int TotalProductsCount { get; init; }

        public int TotalShapesCount { get; init; }
    }
}
