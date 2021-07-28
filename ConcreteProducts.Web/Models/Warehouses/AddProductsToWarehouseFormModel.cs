namespace ConcreteProducts.Web.Models.Warehouses
{
    using System.Collections.Generic;
    using ConcreteProducts.Web.Services.Colors.Dtos;
    using ConcreteProducts.Web.Services.Products.Dtos;

    public class AddProductsToWarehouseFormModel
    {
        public int ProductId { get; init; }

        public int Quantity { get; init; }

        public int ColorId { get; init; }

        public IEnumerable<ProductsInWarehouseViewModel> Products { get; set; }

        public IEnumerable<ColorServiceModel> Colors { get; set; }

    }
}
