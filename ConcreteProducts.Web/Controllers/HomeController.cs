namespace ConcreteProducts.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using ConcreteProducts.Services.Products;
    using ConcreteProducts.Services.Products.Models;
    using ConcreteProducts.Web.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    using static Common.GlobalConstants;

    public class HomeController : Controller
    {
        private readonly IProductService productService;
        private readonly IMemoryCache cache;

        public HomeController(IProductService productService, IMemoryCache cache)
        {
            this.productService = productService;
            this.cache = cache;
        }

        public async Task<IActionResult> Index()
        {
            if (this.User.IsInRole(AdministratorRoleName))
            {
                return this.RedirectToAction("Index", "Admin");
            }

            var latestConcreteProductsKey = "LatestConcreteProductsCacheKey";
            var latestProducts = this.cache.Get<List<ProductListingServiceModel>>(latestConcreteProductsKey);

            if (latestProducts == null)
            {
                latestProducts = await this.productService
                    .GetLatestProductsAsync();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

                this.cache.Set(latestConcreteProductsKey, latestProducts, cacheOptions);
            }

            return this.View(latestProducts);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult StatusCodeErrorPage(int errorCode)
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
