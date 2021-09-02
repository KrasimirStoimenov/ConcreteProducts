namespace ConcreteProducts.Test.Routing.Admin
{
    using ConcreteProducts.Web.Areas.Admin.Controllers;
    using MyTested.AspNetCore.Mvc;
    using NUnit.Framework;

    public class HomeRouteTest
    {
        [Test]
        public void IndexRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin")
                .To<HomeController>(c => c.Index());

        [Test]
        public void ErrorRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Home/Error")
                .To<HomeController>(c => c.Error());
    }
}
