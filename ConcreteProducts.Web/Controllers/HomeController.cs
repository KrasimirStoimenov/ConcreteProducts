namespace ConcreteProducts.Web.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using ConcreteProducts.Web.Models;
    using ConcreteProducts.Web.Services.Products;

    using static Areas.Admin.AdminConstants;

    public class HomeController : Controller
    {
        private readonly IProductService productService;

        public HomeController(IProductService productService)
        {
            this.productService = productService;
        }

        public IActionResult Index()
        {
            if (this.User.IsInRole(AdministratorRoleName))
            {
                return RedirectToAction("Index", "Admin");
            }

            var latest = this.productService.GetLatestProducts();

            return View(latest);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
