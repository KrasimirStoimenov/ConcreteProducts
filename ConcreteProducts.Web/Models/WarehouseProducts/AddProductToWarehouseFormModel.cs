namespace ConcreteProducts.Web.Models.WarehouseProducts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ConcreteProducts.Web.Services.ProductColors.Model;
    using ConcreteProducts.Web.Services.Warehouses.Models;

    using static Data.DataConstants.WarehouseProducts;

    public class AddProductToWarehouseFormModel
    {
        [Range(CountMinValue, CountMaxValue)]
        public int Count { get; init; }

        public int ProductColorId { get; init; }

        public int WarehouseId { get; init; }

        public IEnumerable<ProductColorBaseServiceModel> ProductColors { get; set; }

        public IEnumerable<WarehouseBaseServiceModel> Warehouses { get; set; }
    }
}
