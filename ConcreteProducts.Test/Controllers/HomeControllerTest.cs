namespace ConcreteProducts.Test.Controllers
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;
    using MyTested.AspNetCore.Mvc;

    using ConcreteProducts.Web.Controllers;
    using ConcreteProducts.Web.Services.Products.Models;

    using static Web.Areas.Admin.AdminConstants;

    public class HomeControllerTest
    {
        [Test]
        public void IndexShouldReturnCorrectViewWithModel()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Index())
                .ShouldHave()
                .MemoryCache(cache => cache
                    .ContainingEntry(entry => entry
                        .WithKey("LatestConcreteProductsCacheKey")
                        .WithAbsoluteExpirationRelativeToNow(TimeSpan.FromMinutes(10))
                        .WithValueOfType<List<ProductListingServiceModel>>()))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<ProductListingServiceModel>>());

        [Test]
        public void AdministratorUserShouldBeRedirectedToAnotherHomePage()
            => MyController<HomeController>
                .Instance()
                .WithUser(user => user.InRole(AdministratorRoleName))
                .Calling(c => c.Index())
                .ShouldReturn()
                .RedirectToAction("Index", "Admin");

        [Test]
        public void ErrorMessageShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(e => e.Error())
                .ShouldReturn()
                .View();
    }
}
