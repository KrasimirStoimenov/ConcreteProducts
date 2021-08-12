namespace ConcreteProducts.Web.Areas.Admin.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using ConcreteProducts.Web.Models;

    public class HomeController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
