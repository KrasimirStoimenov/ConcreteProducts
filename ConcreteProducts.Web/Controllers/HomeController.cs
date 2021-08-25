namespace ConcreteProducts.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Diagnostics;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    using ConcreteProducts.Web.Models;
    using ConcreteProducts.Services.Products;
    using ConcreteProducts.Services.Products.Models;

    using static GlobalConstants;

    public class HomeController : Controller
    {
        private readonly IProductService productService;
        private readonly IMemoryCache cache;

        public HomeController(IProductService productService, IMemoryCache cache)
        {
            this.productService = productService;
            this.cache = cache;
        }

        public IActionResult Index()
        {
            if (this.User.IsInRole(AdministratorRoleName))
            {
                return RedirectToAction("Index", "Admin");
            }

            var latestConcreteProductsKey = "LatestConcreteProductsCacheKey";
            var latestProducts = this.cache.Get<List<ProductListingServiceModel>>(latestConcreteProductsKey);

            if (latestProducts == null)
            {
                latestProducts = this.productService
                    .GetLatestProducts()
                    .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

                this.cache.Set(latestConcreteProductsKey, latestProducts, cacheOptions);
            }


            return View(latestProducts);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult StatusCodeErrorPage(int errorCode)
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
