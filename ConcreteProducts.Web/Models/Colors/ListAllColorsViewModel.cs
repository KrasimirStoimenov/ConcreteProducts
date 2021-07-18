namespace ConcreteProducts.Web.Models.Colors
{
    using System.Collections.Generic;

    public class ListAllColorsViewModel : PagingViewModel
    {
        public IEnumerable<ColorListingViewModel> AllColors { get; set; }
    }
}
