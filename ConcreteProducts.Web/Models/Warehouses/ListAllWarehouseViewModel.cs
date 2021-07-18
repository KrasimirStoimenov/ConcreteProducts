namespace ConcreteProducts.Web.Models.Warehouses
{
    using System.Collections.Generic;

    public class ListAllWarehouseViewModel : PagingViewModel
    {
        public IEnumerable<WarehouseListingViewModel> AllWarehouses { get; set; }
    }
}
