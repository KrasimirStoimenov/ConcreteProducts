namespace ConcreteProducts.Web.Areas.Admin.Models.Colors
{
    using System.Collections.Generic;
    using ConcreteProducts.Web.Models;
    using ConcreteProducts.Services.Colors.Models;

    public class ListAllColorsViewModel : PagingViewModel
    {
        public IEnumerable<ColorBaseServiceModel> AllColors { get; set; }
    }
}
