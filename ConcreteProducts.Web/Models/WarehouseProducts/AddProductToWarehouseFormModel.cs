namespace ConcreteProducts.Web.Models.WarehouseProducts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ConcreteProducts.Services.ProductColors.Model;
    using ConcreteProducts.Services.Warehouses.Models;

    using static Common.DataAttributeConstants.WarehouseProducts;

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
