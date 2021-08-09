namespace ConcreteProducts.Web.Models.WarehouseProducts
{
    using System.Collections.Generic;
    using ConcreteProducts.Web.Services.WarehouseProducts.Models;

    public class ListAllProductsInWarehouseViewModel : PagingViewModel
    {
        public IEnumerable<WarehouseProductsServiceModel> ProductsInWarehouse { get; init; }
    }
}
