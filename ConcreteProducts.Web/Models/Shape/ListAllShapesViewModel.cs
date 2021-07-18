namespace ConcreteProducts.Web.Models.Shape
{
    using System.Collections.Generic;

    public class ListAllShapesViewModel : PagingViewModel
    {
        public IEnumerable<ShapeListingViewModel> AllShapes { get; set; }
    }
}
