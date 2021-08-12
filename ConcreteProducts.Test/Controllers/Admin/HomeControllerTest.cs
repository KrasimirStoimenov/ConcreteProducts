namespace ConcreteProducts.Test.Controllers.Admin
{
    using NUnit.Framework;
    using MyTested.AspNetCore.Mvc;

    using ConcreteProducts.Web.Areas.Admin.Controllers;

    class HomeControllerTest
    {
        [Test]
        public void IndexShouldReturnCorrectViewWithModel()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Index())
                .ShouldReturn()
                .View();

        [Test]
        public void ErrorMessageShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(e => e.Error())
                .ShouldReturn()
                .View();
    }
}
