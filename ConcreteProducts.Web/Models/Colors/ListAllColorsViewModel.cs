namespace ConcreteProducts.Web.Models.Colors
{
    using System.Collections.Generic;
    using ConcreteProducts.Web.Services.Colors.Dtos;

    public class ListAllColorsViewModel : PagingViewModel
    {
        public IEnumerable<ColorServiceModel> AllColors { get; set; }
    }
}
