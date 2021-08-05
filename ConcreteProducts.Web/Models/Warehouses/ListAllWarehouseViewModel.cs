namespace ConcreteProducts.Web.Models.Warehouses
{
    using System.Collections.Generic;
    using ConcreteProducts.Web.Models;
    using ConcreteProducts.Web.Services.Warehouses.Models;

    public class ListAllWarehouseViewModel : PagingViewModel
    {
        public IEnumerable<WarehouseWithProductsAndShapesCount> AllWarehouses { get; set; }
    }
}
