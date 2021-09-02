namespace ConcreteProducts.Test.Routing
{
    using ConcreteProducts.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using NUnit.Framework;

    public class HomeRouteTest
    {
        [Test]
        public void IndexRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/")
                .To<HomeController>(c => c.Index());

        [Test]
        public void ErrorRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/Error")
                .To<HomeController>(c => c.Error());
    }
}
