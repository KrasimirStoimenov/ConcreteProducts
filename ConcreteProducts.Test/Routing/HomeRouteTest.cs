namespace ConcreteProducts.Test.Routing
{
    using NUnit.Framework;
    using MyTested.AspNetCore.Mvc;

    using ConcreteProducts.Web.Controllers;

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
