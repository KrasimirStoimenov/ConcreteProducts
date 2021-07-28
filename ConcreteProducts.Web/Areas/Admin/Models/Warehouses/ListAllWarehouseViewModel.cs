namespace ConcreteProducts.Web.Areas.Admin.Models.Warehouses
{
    using System.Collections.Generic;
    using ConcreteProducts.Web.Models;
    using ConcreteProducts.Web.Services.Warehouses.Dtos;

    public class ListAllWarehouseViewModel : PagingViewModel
    {
        public IEnumerable<WarehouseWithProductsAndShapesCount> AllWarehouses { get; set; }
    }
}
