namespace ConcreteProducts.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ShapeHistoriesController : Controller
    {
        public IActionResult SelectShape()
            => this.View();
    }
}
