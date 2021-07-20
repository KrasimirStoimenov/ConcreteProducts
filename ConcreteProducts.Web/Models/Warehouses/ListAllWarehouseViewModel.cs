namespace ConcreteProducts.Web.Models.Warehouses
{
    using System.Collections.Generic;
    using ConcreteProducts.Web.Services.Warehouses.Dtos;

    public class ListAllWarehouseViewModel : PagingViewModel
    {
        public IEnumerable<WarehouseWithProductsAndShapesCount> AllWarehouses { get; set; }
    }
}
