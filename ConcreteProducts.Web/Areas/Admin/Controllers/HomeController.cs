namespace ConcreteProducts.Web.Areas.Admin.Controllers
{
    using System.Diagnostics;

    using ConcreteProducts.Web.Models;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : AdminController
    {
        public IActionResult Index()
        {
            return this.Redirect("/WarehouseProducts/All");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
