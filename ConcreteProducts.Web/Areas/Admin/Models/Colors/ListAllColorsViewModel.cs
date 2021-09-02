namespace ConcreteProducts.Web.Areas.Admin.Models.Colors
{
    using System.Collections.Generic;

    using ConcreteProducts.Services.Colors.Models;
    using ConcreteProducts.Web.Models;

    public class ListAllColorsViewModel : PagingViewModel
    {
        public IEnumerable<ColorBaseServiceModel> AllColors { get; set; }
    }
}
