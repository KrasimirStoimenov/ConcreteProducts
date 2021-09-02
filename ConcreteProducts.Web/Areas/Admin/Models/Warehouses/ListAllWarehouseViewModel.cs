namespace ConcreteProducts.Web.Areas.Admin.Models.Warehouses
{
    using System.Collections.Generic;

    using ConcreteProducts.Services.Warehouses.Models;
    using ConcreteProducts.Web.Models;

    public class ListAllWarehouseViewModel : PagingViewModel
    {
        public IEnumerable<WarehouseWithProductsAndShapesCount> AllWarehouses { get; set; }
    }
}
